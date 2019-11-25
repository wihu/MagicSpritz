using System;
using System.Collections.Generic;
using MagicSpritz;

namespace RM.Hotel
{
    using Middlewares;
    using Modifiers;
    
    class Program
    {
        static void Main(string[] args)
        {
            var store = new Store<PlayerData>();

            var server = new ServerMiddleware();
            var history = new HistoryMiddleware();
            store.AddMiddlewares(server, history);

            var modifiers = new List<Modifier<PlayerData>>();
            modifiers.AddRange(Currency.Modifiers);
            modifiers.AddRange(Inventory.Modifiers);
            store.AddModifiers(modifiers.ToArray());

            // store.Select().Subscribe(x => Console.WriteLine(x));
            store.Select(x => x.Coins).Subscribe(x => Console.WriteLine("Coins: " + x));
            store.Select(x => x.Decos).Subscribe(x => Console.WriteLine("Decos: " + (x == null ? "Empty" : x.Count.ToString())));
            store.Update(new NewGameAction { StartCoins = 5000 });
            store.Update(new BuyDecoAction { TypeId = 1, Cost = 200 });
        }
    }
}
