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
                            state.Coins = action.StartCoins;
                            return state;
                        }
                    ),
                    Create<PlayerData, BuyDecoAction>
                    (
                        (state, action) => 
                        {
                            state.Coins = Math.Max(0, state.Coins - action.Cost);
                            return state;
                        }
                    )
                }
                .ToArray();
            }
        }
    }
}
