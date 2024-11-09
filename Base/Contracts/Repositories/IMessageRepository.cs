using Base.Contracts.Entities;

namespace Base.Contracts.Repositories;

public interface IMessageRepository<T> : IRepository<T> where T : class, IEntity
{
    
}