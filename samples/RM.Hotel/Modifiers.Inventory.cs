using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MagicSpritz;
using static MagicSpritz.Modifiers;

namespace RM.Hotel
{
    public static class Inventory
    {
        public static Modifier<PlayerData>[] Modifiers
        {
            get
            {
                return new List<Modifier<PlayerData>>
                {
                    Create<PlayerData, NewGameAction>
                    (
                        (state, action) => 
                        {
                            state.Decos = ImmutableList<Deco>.Empty;
                            return state;
                        }
                    ),
                    Create<PlayerData, BuyDecoAction>
                    (
                        (state, action) => 
                        {
                            state.Decos = state.Decos.Add(new Deco { TypeId = action.TypeId, Count = 1 });
                            return state;
                        }
                    )
                }
                .ToArray();
            }
        }
    }
}
