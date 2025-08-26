using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace million_api.Models.Entities
{
    public class Owner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public int IdOwner { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public string Birthday { get; set; } = null!;
    }
}
