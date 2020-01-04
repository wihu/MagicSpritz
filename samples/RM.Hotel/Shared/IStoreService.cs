using System;
using MagicOnion;
using MagicOnion.Server;
using MagicSpritz;
using MessagePack;

namespace RM.Hotel
{
public enum EventStatus
{
    Accepted,
    Rejected,
    Resend,
}

[MessagePackObject]
public struct EventResult
{
    [Key(0)]
    public uint Id;
    [Key(1)]
    public EventStatus Status;
}

public interface IStoreService : IService<IStoreService>
{
    UnaryResult<EventResult> SendEvent(ActionEvent t);
}
}
