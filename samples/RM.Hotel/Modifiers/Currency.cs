using System;
using System.Collections.Generic;
using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Modifiers
{
    public static class Currency
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
                            state.Stats.Coins = action.StartCoins;
                            return state;
                        }
                    ),
                    Modifier<PlayerData>.Create<BuyDecoAction>
                    (
                        (state, action) => 
                        {
                            state.Stats.Coins = Math.Max(0, state.Stats.Coins - action.Cost);
                            return state;
                        }
                    )
                };
            }
        }
    }
}
