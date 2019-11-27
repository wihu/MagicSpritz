using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MagicSpritz;
using McMaster.Extensions.CommandLineUtils;

namespace RM.Hotel
{
    using Middlewares;
    using Modifiers;
    
    class Program
    {
        static int Main(string[] args)
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

            var app = new CommandLineApplication();
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

            app.Command("guest", config => 
            {
                config.Command("in", sub =>
                {
                    var guestTypeIdArg = sub.Argument("GuestTypeId", "Guest TypeId");
                    var roomTypeIdArg = sub.Argument("RoomTypeId", "Room TypeId");
                    
                    sub.OnExecute(() => 
                    {
                        int.TryParse(guestTypeIdArg.Value, out int guestTypeId);
                        int.TryParse(roomTypeIdArg.Value, out int roomTypeId);
                        store.Update(new GuestCheckinAction { GuestTypeId = guestTypeId, RoomTypeId = roomTypeId });
                    });
                });
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
                    app.Execute(tokens);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            return 0;
        }
    }
}
