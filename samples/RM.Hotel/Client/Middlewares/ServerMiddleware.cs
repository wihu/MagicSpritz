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
    private Store<PlayerData> _persistedStore;
    private IStoreService _client;
    private StoreLocalPersister _persister;
    private SHA256 _hasher;
    private Channel<ActionEvent> _channel;
    private uint _nextId;

    public ServerMiddleware(Store<PlayerData> store, IStoreService client, StoreLocalPersister persister)
    {
        _store = store;
        _persistedStore = new Store<PlayerData>(store.State, store.Modifiers);
        _client = client;
        _persister = persister;
        _hasher = SHA256.Create();
        _channel = Channel.CreateUnbounded<ActionEvent>(new UnboundedChannelOptions() { SingleReader = true, SingleWriter = true });
        _nextId = 0;

        Task.Run(async () =>
        {
            while (await _channel.Reader.WaitToReadAsync())
            {
                while (_channel.Reader.TryRead(out var e))
                {
                    Console.WriteLine("Read = " + e.Action.GetType().Name);
                    // TODO: handle timeout if no server response.
                    var response = await _client.SendEvent(e);
                    if (response.Id == e.Id)
                    {
                        switch (response.Status)
                        {
                            case EventStatus.Accepted:
                            // persist to local storage.
                            _persistedStore.Update(e.Action);
                            _persister.Set<PlayerData>("player", _persistedStore.State);
                            _persister.Save();
                            break;
                            case EventStatus.Rejected:
                            // discard remaining transactions in channel and revert to last valid store state.
                            
                            break;
                            case EventStatus.Resend:
                            // resend last event.
                            break;
                            default:
                            break;
                        }
                        
                    }
                }
            }
        });
    }

    protected override void BeforeUpdate(IAction action)
    {
    }

    protected override void AfterUpdate(IAction action)
    {
        var bytes = MessagePackSerializer.Serialize<PlayerData>(_store.State);
        var hash = _hasher.ComputeHash(bytes);
        Print(hash);

        var t = new ActionEvent()
        {
            Id = ++_nextId,
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