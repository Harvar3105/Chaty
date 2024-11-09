using Base.Contracts.Entities;
using Base.Contracts.Repositories;

namespace Base.Contracts.Services;

public class BaseService<T> : IService<T> where T : class, IEntity
{
    protected readonly Lazy<IRepository<T>> _repository;

    public BaseService(Lazy<IRepository<T>> repository)
    {
        _repository = repository;
    }

    public Task<List<T>> GetAllAsync()
    {
        return _repository.Value.GetAllAsync();
    }

    public Task<T> GetByIdAsync(string id)
    {
        return _repository.Value.GetByIdAsync(id);
    }

    public Task AddAsync(T entity)
    {
        return _repository.Value.AddAsync(entity);
    }

    public Task UpdateAsync(string id, T updatedEntity)
    {
        return _repository.Value.UpdateAsync(id, updatedEntity);
    }

    public Task DeleteAsync(string id)
    {
        return _repository.Value.DeleteAsync(id);
    }
}