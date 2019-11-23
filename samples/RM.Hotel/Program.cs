using System;
using System.Collections.Immutable;
using MagicSpritz;

namespace RM.Hotel
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new Store<PlayerData>();
            store.AddModifiers(Inventory.Modifiers);
            store.AddModifiers(Currency.Modifiers);
            // store.Select().Subscribe(x => Console.WriteLine(x));
            store.Select(x => x.Coins).Subscribe(x => Console.WriteLine("Coins: " + x));
            store.Select(x => x.Decos).Subscribe(x => Console.WriteLine("Decos: " + (x == null ? "Empty" : x.Count.ToString())));
            store.Update(new NewGameAction { StartCoins = 5000 });
            store.Update(new BuyDecoAction { TypeId = 1, Cost = 200 });
        }
    }
}
