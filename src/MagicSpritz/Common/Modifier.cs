using System;

namespace MagicSpritz
{
    public class Modifier<TState> where TState : new()
    {
        public Func<TState, IAction, TState> Modify { get; protected set; }
        public Type ActionType { get; protected set; }

        public static Modifier<TState> Create<TAction>(Func<TState, TAction, TState> modify) 
        where TAction : class, IAction
        {
            return new Modifier<TState>
            {
                Modify = (state, action) => modify(state, action as TAction),
                ActionType = typeof(TAction)
            };
        }

        public void On<TAction>(Func<TState, TAction, TState> modify)
        where TAction : class, IAction
        {
            Modify = (state, action) => modify(state, action as TAction);
            ActionType = typeof(TAction);
        }
    }

    public class Modifier<TState, TContext> : Modifier<TState> where TState : new()
    {
        public TContext Context { get; private set; }

        public Modifier(TContext context)
        {
            Context = context;
        }

        public void On<TAction>(Func<TContext, TState, TAction, TState> modify)
        where TAction : class, IAction
        {
            Modify = (state, action) => modify(Context, state, action as TAction);
            ActionType = typeof(TAction);
        }
    }
}
