using System.Reflection;
using Base;
using Base.Contracts.Repositories;
using DAL.Domain;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Attributes;

namespace DAL.Repositories;

public class ChatRepository : BaseRepository<Chat>, IChatRepository<Chat>
{
    public ChatRepository(IMongoDbContext database) : base(database, typeof(Chat).GetCustomAttribute<CollectionNameAttribute>()!.Name)
    {
    }

    public new async Task AddAsync(Chat entity)
    {
        var stored = await Collection.FindAsync(e => e.ChatName.Equals(entity.ChatName)
        && e.AdminId!.Equals(entity.AdminId));

        if (await stored.AnyAsync())
        {
            foreach (var chat in stored.ToList())
            {
                bool isSimilar = true;
                foreach (var user in chat.Users)
                {
                    if (!entity.Users.Contains(user)) isSimilar = false;
                }
                if (isSimilar) return;
            }
        }
        
        await base.AddAsync(entity);
    }
}