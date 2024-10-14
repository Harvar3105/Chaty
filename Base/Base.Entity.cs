using System.ComponentModel.DataAnnotations;
using Base.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Base.Domain;

public abstract class BaseEntity : IEntity
{
    [Key]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime? CreationDate { get; set; }
}