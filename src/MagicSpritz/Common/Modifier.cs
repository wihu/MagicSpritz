using System;

namespace MagicSpritz
{
    public class Modifier<TState> where TState : new()
    {
        public Func<TState, IAction, TState> Modify { get; set; }
        internal Type Type { get; set; }
    }

    public static class Modifiers
    {
        public static Modifier<TState> Create<TState, TAction>(Func<TState, TAction, TState> modify) 
        where TState : new() 
        where TAction : class
        {
            return new Modifier<TState>
            {
                Modify = (state, action) => modify(state, action as TAction),
                Type = typeof(TAction)
            };
        }
    }
}
