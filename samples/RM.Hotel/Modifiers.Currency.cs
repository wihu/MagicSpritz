using System;
using System.Collections.Generic;
using MagicSpritz;
using static MagicSpritz.Modifiers;

namespace RM.Hotel
{
    public static class Currency
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
                            state.Coins = 1000;
                            return state;
                        }
                    ),
                    Create<PlayerData, BuyDecoAction>
                    (
                        (state, action) => 
                        {
                            state.Coins = Math.Max(0, state.Coins - 5);
                            return state;
                        }
                    )
                }
                .ToArray();
            }
        }
    }
}
