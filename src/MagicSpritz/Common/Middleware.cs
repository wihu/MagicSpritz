using System;

namespace MagicSpritz
{
    
public class Middleware<TState> : IStoreUpdater where TState : new()
{
    public IStoreUpdater Next { get; set; }

    public Middleware()
    {
    }

    void IStoreUpdater.UpdateStore(IAction action)
    {
        BeforeUpdate(action);
        Next.UpdateStore(action);
        AfterUpdate(action);
    }

    protected virtual void BeforeUpdate(IAction action)
    {

    }

    protected virtual void AfterUpdate(IAction action)
    {

    }
}
}
