using System;

namespace MagicSpritz
{
    public class Modifier<TState> where TState : new()
    {
        public Func<TState, IAction, TState> Modify { get; private set; }
        public Type ActionType { get; private set; }

        public static Modifier<TState> Create<TAction>(Func<TState, TAction, TState> modify) 
        where TAction : class, IAction
        {
            return new Modifier<TState>
            {
                Modify = (state, action) => modify(state, action as TAction),
                ActionType = typeof(TAction)
            };
        }
    }
}
