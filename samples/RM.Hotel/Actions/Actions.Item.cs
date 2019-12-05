using System.Collections.Generic;
using MagicSpritz;
using RM.Hotel.Models;

namespace RM.Hotel.Actions
{
public class MakeItemAction : IAction
{
    public int MachineId;
    public int ItemId;
}

public class CollectItemsAction : IAction
{
    public List<int> ItemIds;
}
}
