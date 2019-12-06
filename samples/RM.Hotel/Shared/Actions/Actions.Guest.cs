using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Actions
{
public class GuestReserveAction : IAction
{
    public int GuestId;
}

public class GuestCheckInAction : IAction
{
    public int GuestId;
    public int RoomId;
}

public class GuestCheckOutAction : IAction
{
    public int GuestId;
}
}
