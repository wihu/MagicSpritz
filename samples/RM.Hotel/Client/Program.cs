using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using MagicSpritz;
using McMaster.Extensions.CommandLineUtils;
using MagicOnion.Client;
using Grpc.Core;
using MessagePack;
using MessagePack.Resolvers;

namespace RM.Hotel
{
    using Middlewares;
    using Modifiers;
    
    class Program
    {

        static void RegisterResolvers()
        {
            CompositeResolver.RegisterAndSetAsDefault
            (
                MagicOnion.Resolvers.MagicOnionResolver.Instance,
                MessagePack.Resolvers.GeneratedResolver.Instance,
                MessagePack.Resolvers.Client.GeneratedResolver.Instance,
                BuiltinResolver.Instance,
                PrimitiveObjectResolver.Instance
            );
        }

        static void ShowHotelStatus(Store<Models.PlayerData> store)
        {
            var token = store.Select(x => x.Hotel).Subscribe(hotel => 
            {
                Console.WriteLine(hotel.Name);
                Console.WriteLine(hotel.Level);
                if (hotel.Rooms == null)
                {
                    Console.WriteLine("Rooms: Empty");
                }
                else
                {
                    hotel.Rooms.ForEach(r => Console.WriteLine($"Room[{r.TypeId}] => Level {r.Level}"));
                }
            });
            token.Dispose();
        }

        static async Task Main(string[] args)
        {
            const int kDefaultStartCoins = 5000;
            var gameConfig = new GameConfig();
            var configProvider = new ConfigProvider();
            configProvider.Add("default", gameConfig);

            var store = new Store<Models.PlayerData>();

            var server = new ServerMiddleware();
            var history = new HistoryMiddleware();
            store.AddMiddlewares(server, history);

            var guest = new GuestModifierProvider(new GuestModifierFactory(configProvider));

            var modifiers = new List<Modifier<Models.PlayerData>>();
            modifiers.AddRange(Currency.Modifiers);
            modifiers.AddRange(Inventory.Modifiers);
            modifiers.AddRange(Hotel.Modifiers);
            modifiers.AddRange(guest.Modifiers);
            store.AddModifiers(modifiers.ToArray());

            // store.Select().Subscribe(x => Console.WriteLine(x));
            store.Select(x => x.Stats.Coins).Subscribe(x => Console.WriteLine("Coins: " + x));
            store.Select(x => x.Inventory.Items).Subscribe(x => Console.WriteLine("Decos: " + (x == null ? "Empty" : x.Sum(x => x.Value.Count).ToString())));
            // store.Select(x => x.Hotel.Rooms).Subscribe(x => x.ForEach(r => Console.WriteLine($"Room[{r.TypeId}] => Level {r.Level}")));
            store.Select(x => x.Hotel.Rooms).Subscribe(x => 
            {
                if (x == null)
                {
                    Console.WriteLine("Rooms: Empty");
                }
                else
                {
                    x.ForEach(r => Console.WriteLine($"Room[{r.TypeId}] => Level {r.Level}"));
                }
            });

            store.Update(new NewGameAction { StartCoins = kDefaultStartCoins });

            var app = new Game(store);
            app.Command("newgame", config => 
            {
                var coins = config.Option("--coins", "Starting coins", CommandOptionType.SingleValue);

                config.OnExecute(() => 
                {
                    if (!int.TryParse(coins.Value(), out int val))
                    {
                        val = kDefaultStartCoins;
                    }
                                        
                    store.Update(new NewGameAction { StartCoins = val });
                });
            });

            app.Command("buy", config => 
            {
                var typeIdArg = config.Argument("TypeId", "Item TypeId");
                var costArg = config.Argument("Cost", "Item Cost");

                config.OnExecute(() => 
                {
                    int.TryParse(typeIdArg.Value, out int typeId);
                    int.TryParse(costArg.Value, out int cost);
                    store.Update(new BuyDecoAction { TypeId = typeId, Cost = cost });
                });
            });

            app.Command("room", config => 
            {
                config.Command("up", sub =>
                {
                    const int kUpgradeRoomCost = 100;
                    var typeIdArg = sub.Argument("TypeId", "Item TypeId");

                    sub.OnExecute(() => 
                    {
                        int.TryParse(typeIdArg.Value, out int typeId);
                        store.Update(new UpgradeRoomAction { TypeId = typeId, Cost = kUpgradeRoomCost });
                    });
                });
            });

            app.AddCommand<int, int>("guest", "cin", (guestId, roomId) => new GuestCheckinAction { GuestTypeId = guestId, RoomTypeId = roomId });
            app.AddCommand<int>("guest", "cout", (guestId) => new GuestCheckoutAction { GuestTypeId = guestId });
            app.AddCommand("hotel", "status", () => ShowHotelStatus(store));

            MagicOnion.MagicOnionInitializer.Register();
            RegisterResolvers();
            var channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
            var client = MagicOnionClient.Create<IStoreService>(channel);

            app.AddAsyncCommand("sync", "new", async () => 
            {
                var t = new Transaction
                {
                    Id = 1,
                    Hash = "abc",
                    Action = new NewGameAction { StartCoins = 250 }
                };

                var result = await client.Update(t);
                Console.WriteLine("sync result = " + result);
            });

            app.AddCommand("sync", "test", () => 
            {
                var t = new Transaction
                {
                    Id = 1,
                    Hash = "abc",
                    Action = new NewGameAction { StartCoins = 250 }
                };

                var bytes = MessagePackSerializer.Serialize(t);
                var json = MessagePackSerializer.ToJson(bytes);
                var result = MessagePackSerializer.Deserialize<Transaction>(bytes);
                var newGame = result.Action as NewGameAction;
                Console.WriteLine("sync result = " + json + " => " + result.ToString());
            });

            var config = new StoreLocalPersister.Config { FilePath = "LocalData/Player.txt", TextFormat = true };
            var persister = new StoreLocalPersister(config);
            app.AddCommand("data", "save", () =>
            {
                persister.Set<Models.PlayerData>("player", store.State);
                persister.Save();
            });
            app.AddCommand("data", "load", () =>
            {
                persister.Load();
                persister.Get<Models.PlayerData>("player", out var player);
                store.Update(new LoadGameAction { State = player });
            });

            app.Command("timer", config =>
            {
                config.OnExecute(() => 
                {
                    var start = DateTime.Now;
                    var prev = start;
                    Console.Write("Timer: " + 0);
                    Console.CursorVisible = false;
                    while (!Console.KeyAvailable)
                    {
                        var now = DateTime.Now;
                        double dt = (now - prev).TotalSeconds;
                        if (dt >= 1.0)
                        {
                            prev = now;
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write("Timer: " + (int)(now - start).TotalSeconds);
                        }
                    }
                    Console.ReadKey(true);
                    Console.WriteLine();
                });
            });

            while (true)
            {
                var command = Prompt.GetString("Command", "#");
                if (command == "bye")
                {
                    Console.WriteLine("bye!");
                    break;
                }

                var tokens = command.Split(" ");
                
                try
                {
                    await app.ExecuteAsync(tokens);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.GetType()}: {e.Message}");
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
