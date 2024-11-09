using Base.Contracts.Entities;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace Base.Contracts.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly IMongoCollection<T> Collection;

    protected BaseRepository(IMongoDbContext ctx, string collectionName)
    {
        Collection = ctx.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Collection.Find(entity => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await Collection.Find(entity => entity.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        entity.CreationDate = DateTime.UtcNow;
        await Collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, T updatedEntity)
    {
        await Collection.ReplaceOneAsync(entity => entity.Id.Equals(id), updatedEntity);
    }

    public async Task DeleteAsync(string id)
    {
        await Collection.DeleteOneAsync(entity => entity.Id.Equals(id));
    }
}