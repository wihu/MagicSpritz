using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MagicSpritz;

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
                            state.Decos = ImmutableList<Deco>.Empty;
                            return state;
                        }
                    ),
                    Modifier<PlayerData>.Create<BuyDecoAction>
                    (
                        (state, action) => 
                        {
                            state.Decos = state.Decos.Add(new Deco { TypeId = action.TypeId, Count = 1 });
                            return state;
                        }
                    )
                };
            }
        }
    }
}
