using Base.Contracts.Entities;

namespace Base.Contracts.Repositories;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<List<T>> GetAllAsync();

    Task<T> GetByIdAsync(string id);

    Task AddAsync(T entity);

    Task UpdateAsync(string id, T updatedEntity);

    Task DeleteAsync(string id);
}