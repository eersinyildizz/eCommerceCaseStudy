using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HepsiBuradaCaseStudy.Domain.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement(Order = 0)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    }
}
