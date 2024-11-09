using Base.Contracts.Entities;

namespace Base.Contracts.Services;

public interface IMessageService<T> : IService<T> where T : class, IEntity
{
    
}