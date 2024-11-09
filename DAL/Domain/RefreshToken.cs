using Base;
using Base.Contracts.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace DAL.Domain;

[CollectionName("RefreshTokens")]
public class RefreshToken : BaseRefreshToken
{
    public string UserId { get; set; }
    [BsonIgnore]
    public User? User { get; set; }
}