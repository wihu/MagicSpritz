using System;
using System.Threading;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Server;
using RM.Hotel;
using System.Security.Cryptography;

namespace RM.Hotel.Server
{
    public class StoreService : ServiceBase<IStoreService>, IStoreService
    {
        private SHA256 _hasher = SHA256.Create();
        
        UnaryResult<EventResult> IStoreService.SendEvent(ActionEvent t)
        {
            Console.WriteLine(t.ToString());
            // fake hash calculation just for testing, should hash from store state.
            // var bytes = System.Text.Encoding.UTF8.GetBytes(t.ToString());
            // t.Hash = Convert.ToBase64String(_hasher.ComputeHash(bytes));
            var response = new EventResult { Id = t.Id, Status = EventStatus.Accepted };

            return new UnaryResult<EventResult>(response);
        }
    }
}
