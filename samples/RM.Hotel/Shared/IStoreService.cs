using System;
using MagicOnion;
using MagicOnion.Server;
using MagicSpritz;

namespace RM.Hotel
{
public interface IStoreService : IService<IStoreService>
{
    UnaryResult<Transaction> Update(Transaction t);
}
}
