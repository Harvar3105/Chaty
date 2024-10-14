using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Base.Interfaces;

public interface IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime? CreationDate { get; set; }
}