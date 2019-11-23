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
            store.Select().Subscribe(x => Console.WriteLine(x));
            store.Update(new NewGameAction());
            store.Update(new BuyDecoAction());
        }
    }
}
