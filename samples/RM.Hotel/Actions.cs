using System.Collections.Generic;
using MagicSpritz;

namespace RM.Hotel
{
using Models;
public class NewGameAction : IAction
{
    public int StartCoins;
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
