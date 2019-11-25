﻿using System;
using System.Collections.Generic;
using System.Linq;
using MagicSpritz;

namespace RM.Hotel
{
    using Middlewares;
    using Modifiers;
    
    class Program
    {
        static void Main(string[] args)
        {
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
            store.Update(new NewGameAction { StartCoins = 5000 });
            store.Update(new BuyDecoAction { TypeId = 1, Cost = 200 });
            store.Update(new BuyDecoAction { TypeId = 1, Cost = 200 });
            store.Update(new BuyDecoAction { TypeId = 2, Cost = 100 });
        }
    }
}
