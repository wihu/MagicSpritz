using MagicSpritz;

namespace RM.Hotel
{
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
}
