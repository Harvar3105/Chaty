using Base;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Domain;

public class RefreshToken : BaseRefreshToken
{
    public string UserId { get; set; }
    [BsonIgnore]
    public User? User { get; set; }
}