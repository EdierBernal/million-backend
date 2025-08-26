using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace million_api.Models.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public int IdProperty { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Price { get; set; }
        public string CodeInternal { get; set; } = null!;
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
}
