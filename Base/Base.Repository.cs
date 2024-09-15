using Base.Domain;
using MongoDB.Driver;

namespace Base;

public class Base_Repository<T>
    where T : Base_Entity
{
    protected readonly IMongoCollection<T> _collection;

    public Base_Repository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(entity => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _collection.Find(entity => entity.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, T updatedEntity)
    {
        await _collection.ReplaceOneAsync(entity => entity.Id.Equals(id), updatedEntity);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(entity => entity.Id.Equals(id));
    }
}