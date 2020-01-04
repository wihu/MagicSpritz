using MagicSpritz;
using MessagePack;

namespace RM.Hotel
{
    [MessagePackObject]
    public class ActionEvent
    {
        [Key(0)]
        public uint Id;
        [Key(1)]
        public string Hash;
        [Key(2)]
        public IAction Action;

        public override string ToString()
        {
            return $"Id = {Id}, Hash = {Hash}, Action = {Action.GetType().Name}";
        }
    }
}
