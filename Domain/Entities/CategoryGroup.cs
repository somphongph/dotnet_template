using Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class Sample : BaseMongoEntity
{
    [BsonElement("code")]
    public string Code { get; set; } = string.Empty;

    [BsonElement("name")]
    public Locale Name { get; set; } = new Locale();
}
