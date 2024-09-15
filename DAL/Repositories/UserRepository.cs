using Base;
using DAL.Domain;
using MongoDB.Driver;

namespace DAL.Repositories;

public class UserRepository : Base_Repository<User>
{
    public UserRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
}