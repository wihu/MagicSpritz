using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading.Channels;
using MagicSpritz;
using MessagePack;
using RM.Hotel.Models;

namespace RM.Hotel.Middlewares
{
public class ServerMiddleware : Middleware<PlayerData>
{
    private Store<PlayerData> _store;
    private IStoreService _client;
    private SHA256 _hasher;
    private Channel<Transaction> _channel;

    public ServerMiddleware(Store<PlayerData> store, IStoreService client)
    {
        _store = store;
        _client = client;
        _hasher = SHA256.Create();
        _channel = Channel.CreateUnbounded<Transaction>(new UnboundedChannelOptions() { SingleReader = true, SingleWriter = true });
        Task.Run(async () =>
        {
            while (await _channel.Reader.WaitToReadAsync())
            {
                while (_channel.Reader.TryRead(out var t))
                {
                    Console.WriteLine("Read = " + t.Action.GetType().Name);
                    var result = await _client.Update(t);
                    if (result.Hash == t.Hash)
                    {
                        // persist to local storage.
                    }
                    else
                    {
                        // discard remaining transactions in channel and revert to last valid store state.
                    }
                }
            }
        });
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

        var t = new Transaction()
        {
            Id = 0,
            Action = action,
            Hash = Convert.ToBase64String(hash)
        };
        _channel.Writer.TryWrite(t);

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