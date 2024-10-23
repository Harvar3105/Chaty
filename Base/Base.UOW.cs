using MongoDbGenericRepository;

namespace Base;

public abstract class BaseUow<TContext>(IMongoDbContext ctx)
    where TContext : class
{
    protected readonly IMongoDbContext Ctx = ctx;
}