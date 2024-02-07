using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Library.Models;

public class RateBook
{      
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    public int Rating { get; set; }
}