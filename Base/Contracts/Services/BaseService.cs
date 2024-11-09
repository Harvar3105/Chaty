using Base.Contracts.Entities;
using Base.Contracts.Repositories;

namespace Base.Contracts.Services;

public class BaseService<T> : IService<T> where T : class, IEntity
{
    protected readonly IRepository<T> _repository;

    public BaseService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public Task<List<T>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<T> GetByIdAsync(string id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task AddAsync(T entity)
    {
        return _repository.AddAsync(entity);
    }

    public Task UpdateAsync(string id, T updatedEntity)
    {
        return _repository.UpdateAsync(id, updatedEntity);
    }

    public Task DeleteAsync(string id)
    {
        return _repository.DeleteAsync(id);
    }
}