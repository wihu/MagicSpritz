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

            var store = new Store<Models.PlayerData>();

            var server = new ServerMiddleware();
            var history = new HistoryMiddleware();
            store.AddMiddlewares(server, history);

            var modifiers = new List<Modifier<Models.PlayerData>>();
            modifiers.AddRange(Currency.Modifiers);
            modifiers.AddRange(Inventory.Modifiers);
            store.AddModifiers(modifiers.ToArray());

            // store.Select().Subscribe(x => Console.WriteLine(x));
            store.Select(x => x.Stats.Coins).Subscribe(x => Console.WriteLine("Coins: " + x));
            store.Select(x => x.Inventory.Items).Subscribe(x => Console.WriteLine("Decos: " + (x == null ? "Empty" : x.Sum(x => x.Value.Count).ToString())));
            store.Update(new NewGameAction { StartCoins = kDefaultStartCoins });
            // store.Update(new BuyDecoAction { TypeId = 1, Cost = 200 });
            // store.Update(new BuyDecoAction { TypeId = 1, Cost = 200 });
            // store.Update(new BuyDecoAction { TypeId = 2, Cost = 100 });

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

            app.Command("time", config =>
            {
                config.OnExecute(() => 
                {
                    var start = DateTime.Now;
                    var prev = start;
                    Console.Write("Time: " + 0);
                    Console.CursorVisible = false;
                    while (!Console.KeyAvailable)
                    {
                        var now = DateTime.Now;
                        double dt = (now - prev).TotalSeconds;
                        if (dt >= 1.0)
                        {
                            prev = now;
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write("Time: " + (int)(now - start).TotalSeconds);
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
