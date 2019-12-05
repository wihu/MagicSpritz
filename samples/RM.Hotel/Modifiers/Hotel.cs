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
        public class HotelModifierContext
        {

        }

        public class ModifierFactory<TState, TContext> where TState : new()
        {
            protected TContext _context;

            public Modifier<TState> Create<TAction>(Func<TContext, TState, TAction, TState> modify)
            where TAction : class, IAction
            {
                var modifier = new Modifier<TState, TContext>(_context);
                modifier.On<TAction>(modify);
                return modifier;
            }
        }

        public class HotelModifierProvider
        {
            private ModifierFactory<PlayerData, HotelModifierContext> _factory;

            public List<Modifier<PlayerData>> Modifiers
            {
                get
                {
                    return new List<Modifier<PlayerData>>
                    {
                        _factory.Create<RM.Hotel.Actions.BuildMachineAction>
                        (
                            (context, state, action) => 
                            {
                                var machine = new Machine { TypeId = action.TypeId };
                                state.Hotel.Machines.Add(machine);
                                return state;
                            }
                        )
                    };
                }
            }
        }

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
                            state.Hotel.Name = "My Hotel";
                            state.Hotel.Level = 1;   
                            state.Hotel.Rooms = rooms.ToImmutableList();
                            state.Hotel.Machines = ImmutableList<Machine>.Empty;
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
