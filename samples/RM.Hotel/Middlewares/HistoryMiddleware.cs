using System;
using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Middlewares
{
public class HistoryMiddleware : Middleware<PlayerData>
{
    protected override void BeforeUpdate(IAction action)
    {
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("[History] Before Update = " + action.GetType());
    }

    protected override void AfterUpdate(IAction action)
    {
        // Console.WriteLine("[History] After Update = " + action.GetType());
        Console.WriteLine("---------------------------------------------------");
    }
}
}
