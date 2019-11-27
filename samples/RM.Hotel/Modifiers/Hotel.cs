using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Modifiers
{
    public static class Hotel
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
                            var rooms = new List<Room>();
                            for (int i = 1; i <= 3; ++i)
                            {
                                rooms.Add(new Room { TypeId = i });
                            }
                            state.Hotel.Level = 1;   
                            state.Hotel.Rooms = rooms.ToImmutableList();
                            return state;
                        }
                    ),
                    Modifier<PlayerData>.Create<UpgradeRoomAction>
                    (
                        (state, action) => 
                        {
                            const int kMaxLevel = 5;
                            int k = state.Hotel.Rooms.FindIndex(x => x.TypeId == action.TypeId);
                            if (k >= 0)
                            {
                                var room = state.Hotel.Rooms.ElementAt(k);
                                room.Level = Math.Min(room.Level + 1, kMaxLevel);
                                state.Hotel.Rooms = state.Hotel.Rooms.SetItem(k, room);
                            }
                            return state;
                        }
                    )
                };
            }
        }
    }
}
