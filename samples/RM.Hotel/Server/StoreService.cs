using System;
using System.Threading;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Server;
using RM.Hotel;

namespace RM.Hotel.Server
{
    public class StoreService : ServiceBase<IStoreService>, IStoreService
    {
        UnaryResult<int> IStoreService.Update(Transaction t)
        {
            return new UnaryResult<int>(1234);
        }
    }
}
