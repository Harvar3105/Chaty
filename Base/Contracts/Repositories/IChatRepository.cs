using Base.Contracts.Entities;

namespace Base.Contracts.Repositories;

public interface IChatRepository<T> : IRepository<T> where T : class, IEntity
{
    
}