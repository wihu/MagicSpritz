using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Modifiers
{
    public static class Inventory
    {
        public static List<Modifier<PlayerData>> Modifiers
        {
            get
            {
                return new List<Modifier<PlayerData>>
                {
                    Modifier<PlayerData>.Create<NewGameAction>
                    (
                        (state, action) => 
                        {
                            state.Inventory.Items = ImmutableDictionary<int, Item>.Empty;
                            return state;
                        }
                    ),
                    Modifier<PlayerData>.Create<BuyDecoAction>
                    (
                        (state, action) => 
                        {
                            var newItem = new Item { TypeId = action.TypeId, Count = 1};
                            state.Inventory.Items.TryGetValue(newItem.TypeId, out var item);
                            newItem.Count += item.Count;
                            state.Inventory.Items = state.Inventory.Items.SetItem(newItem.TypeId, newItem);
                            return state;
                        }
                    )
                };
            }
        }
    }
}
