using Base;
using DAL.Domain;
using MongoDB.Driver;

namespace DAL.Repositories;

public class PasswordRepository : Base_Repository<Password>
{
    public PasswordRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
}