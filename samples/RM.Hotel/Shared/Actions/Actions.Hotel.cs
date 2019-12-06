using MagicSpritz;

namespace RM.Hotel.Actions
{
public class BuildFloorAction : IAction
{
}

public class BuildRoomAction : IAction
{
    public int RoomId;
}

public class UpgradeRoomAction : IAction
{
    public int RoomId;
}

public class UpgradeStorageAction : IAction
{

}

public class BuildMachineAction : IAction
{
    public int TypeId;
}
public class UpgradeMachineAction : IAction
{
    public int TypeId;
    public int InstanceId;
}

}
