using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Modifiers
{
    public class GuestModifierContext
    {
        public readonly ConfigProvider ConfigProvider;

        public GuestModifierContext(ConfigProvider configProvider)
        {
            ConfigProvider = configProvider;
        }
    }

    public class GuestModifierFactory
    {
        private GuestModifierContext _context;

        public GuestModifierFactory(ConfigProvider configProvider)
        {
            _context = new GuestModifierContext(configProvider);
        }

        public Modifier<PlayerData> Create<TAction>(Func<GuestModifierContext, PlayerData, TAction, PlayerData> modify)
        where TAction : class, IAction
        {
            var modifier = new Modifier<PlayerData, GuestModifierContext>(_context);
            modifier.On<TAction>(modify);
            return modifier;
        }
    }

    public class GuestModifierProvider
    {
        private GuestModifierFactory _factory;

        public GuestModifierProvider(GuestModifierFactory factory)
        {
            _factory = factory;
        }

        public List<Modifier<PlayerData>> Modifiers
        {
            get
            {
                return new List<Modifier<PlayerData>>
                {
                    _factory.Create<GuestCheckinAction>
                    (
                        (context, state, action) => 
                        {
                            var gameConfig = context.ConfigProvider.Get<GameConfig>("default");
                            Console.WriteLine($"Guest[{action.GuestTypeId}] check-in => Room[{action.RoomTypeId}]");
                            Console.WriteLine("Max floor = " + gameConfig.MaxHotelFloor);
                            return state;
                        }
                    ),
                    _factory.Create<GuestCheckoutAction>
                    (
                        (context, state, action) => 
                        {
                            Console.WriteLine($"Guest[{action.GuestTypeId}] check-out");
                            return state;
                        }
                    )
                };
            }
        }
    }
}
