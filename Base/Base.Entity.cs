using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Base.Domain;

public abstract class Base_Entity
{
    [BsonId(IdGenerator = typeof(AscendingGuidGenerator))]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
}