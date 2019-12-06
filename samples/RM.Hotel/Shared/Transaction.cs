using MagicSpritz;
using MessagePack;

namespace RM.Hotel
{
    [MessagePackObject]
    public class Transaction // Change to ActionPayload
    {
        [Key(0)]
        public int Id;
        [Key(1)]
        public string Hash;
        [Key(2)]
        public IAction Action;

        public override string ToString()
        {
            return $"Id = {Id}, Hash = {Hash}, Action = {Action}";
        }
    }
}
