using System.Collections.Immutable;

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

public struct Room
{
    public int TypeId;
    public int Level;
    public ImmutableList<Deco> Decos;
}

public struct Task
{
    public int TypeId;
    public int RewardCoins;
    public int StartTimeSec;
}

public struct Guest
{
    public int TypeId;
    public int RoomId;
    public int TaskId;
    public int Level;
    public ImmutableList<Item> RequiredDecos;
}

public struct Hotel
{
    public int Level;
    public ImmutableList<Guest> Queue;
    public ImmutableList<Room> Rooms;
}

public struct Stats
{
    public int Level;
    public int Xp;
    public int Coins;
}

public struct PlayerData
{
    public Stats Stats;
    public Hotel Hotel;
    public Inventory Inventory;
    public ImmutableList<Task> Tasks;
}
}
