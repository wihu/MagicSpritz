using System.Collections.Immutable;

namespace RM.Hotel
{
public struct Deco
{
    public int TypeId;
    public int Count;
}

public struct PlayerData
{
    public int Coins;
    public ImmutableList<Deco> Decos;

    public override string ToString()
    {
        return $"[Player] Coins = {Coins} Decos = {(Decos == null ? "Empty" : Decos.Count.ToString())}";
    }
}
}
