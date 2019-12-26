using System;
using MagicSpritz;
using RM.Hotel.Models;
using System.Security.Cryptography;
using MessagePack;

namespace RM.Hotel.Middlewares
{
public class ServerMiddleware : Middleware<PlayerData>
{
    private Store<PlayerData> _store;
    private SHA256 _hasher;

    public ServerMiddleware(Store<PlayerData> store)
    {
        _store = store;
        _hasher = SHA256.Create();
    }

    protected override void BeforeUpdate(IAction action)
    {
        // Console.WriteLine("[Server] Before Update = " + action.GetType());
    }

    protected override void AfterUpdate(IAction action)
    {
        // Console.WriteLine("[Server] After Update = " + action.GetType());

        var bytes = MessagePackSerializer.Serialize<PlayerData>(_store.State);
        var hash = _hasher.ComputeHash(bytes);
        Print(hash);
    }

    private static void Print(byte[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"{array[i]:x2}");
        }
        Console.WriteLine();
    }
}
}