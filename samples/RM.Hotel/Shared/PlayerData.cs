using System.Collections.Immutable;
using MessagePack;

namespace RM.Hotel.Models
{
public struct Item
{
    public int TypeId;
    public int Count;
}
public struct Inventory
{
    public ImmutableDictionary<int, Item> Items;
}

public struct Placement
{
    public int X, Y, Z, Dir;
}

public struct Deco
{
    public int TypeId;
    public Placement Placement;
}

[MessagePackObject]
public struct Room
{
    [Key(0)]
    public int TypeId;
    [Key(1)]
    public int Level;
    [IgnoreMember]
    public ImmutableList<Deco> Decos;
}

// public struct Task
// {
//     public int TypeId;
//     public int RewardCoins;
//     public int StartTimeSec;
// }

public struct Guest
{
    public int TypeId;
    public int RoomId;
    public int TaskId;
    public int Level;
    public uint StartStayTimeSec;
    public ImmutableList<Item> RequiredDecos;
}

public struct Timer
{
    public int DurationSec;
    public int StartTimeSec;
}

public struct ItemQueue
{
    public int TypeId;
    public Timer Timer;
}

public struct Machine
{
    public int TypeId;
    public int InstanceId;
    public int Level;
    public ImmutableList<ItemQueue> Queue;
}

[MessagePackObject]
public struct Hotel
{
    [Key(0)]
    public string Name;
    [Key(1)]
    public int Level;
    // [Key(2)]
    [IgnoreMember]
    public ImmutableList<Guest> Queue;
    [Key(3)]
    public ImmutableList<Room> Rooms;
    // [Key(4)]
    [IgnoreMember]
    public ImmutableList<Machine> Machines;
}

[MessagePackObject]
public struct Stats
{
    [Key(0)]
    public int Level;
    [Key(1)]
    public int Xp;
    [Key(2)]
    public int Coins;
}

[MessagePackObject]
public struct PlayerData
{
    [Key(0)]
    public Stats Stats;
    [Key(1)]
    public Hotel Hotel;
    // [Key(2)]
    [IgnoreMember]
    public Inventory Inventory;
    // [Key(3)]
    // [IgnoreMember]
    // public ImmutableList<Task> Tasks;

    public override string ToString()
    {
        return $"[PlayerData] Coins = {Stats.Coins}";
    }
}
}
