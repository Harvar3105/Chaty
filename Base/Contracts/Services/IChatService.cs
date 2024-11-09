using Base.Contracts.Entities;

namespace Base.Contracts.Services;

public interface IChatService<T> : IService<T> where T : class, IEntity
{
    
}