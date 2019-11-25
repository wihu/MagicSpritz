using System;
using MagicSpritz;

namespace RM.Hotel.Middlewares
{
public class ServerMiddleware : Middleware<PlayerData>
{
    protected override void BeforeUpdate(IAction action)
    {
        // Console.WriteLine("[Server] Before Update = " + action.GetType());
    }

    protected override void AfterUpdate(IAction action)
    {
        Console.WriteLine("[Server] After Update = " + action.GetType());
    }
}
}