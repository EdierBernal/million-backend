using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace million_api.Models.Entities
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; } = null!;
        public bool Enabled { get; set; } = false!;
    }
}
