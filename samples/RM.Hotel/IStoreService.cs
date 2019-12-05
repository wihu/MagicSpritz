using System;
using MagicOnion;
using MagicOnion.Server;
using MagicSpritz;

namespace RM.Hotel
{
public interface IStoreService : IService<IStoreService>
{
    UnaryResult<int> Update(Transaction t);
}
}
