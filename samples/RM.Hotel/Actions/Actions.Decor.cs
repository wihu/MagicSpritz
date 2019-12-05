using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Actions
{
public class DecorPlaceAction : IAction
{
    public int TypeId;
    public int RoomId;
    public Placement Placement;
}

public class DecorBuyAction : IAction
{
    public int TypeId;
    public int Count;
}
}
