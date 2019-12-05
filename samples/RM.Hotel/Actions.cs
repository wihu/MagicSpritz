using System.Collections.Generic;
using MagicSpritz;
using MessagePack;

namespace RM.Hotel
{
using Models;
[MessagePackObject]
public class NewGameAction : IAction
{
    [Key(0)]
    public int StartCoins;

    public override string ToString()
    {
        return $"[StartCoins = {StartCoins}]";
    }
}
public class BuyDecoAction : IAction
{
    public int TypeId;
    public int Cost;
}

public class UpgradeRoomAction : IAction
{
    public int TypeId;
    public int Cost;
}

public class GuestQueueAction : IAction
{
    public int GuestTypeId;
    public List<Item> RequiredDecos;
}

public class GuestCheckinAction : IAction
{
    public int GuestTypeId;
    public int RoomTypeId;
}

public class GuestCheckoutAction : IAction
{
    public int GuestTypeId;
}
}
