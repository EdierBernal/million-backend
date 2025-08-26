using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace million_api.Models.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string IdPropertyTrace { get; set; } = null!;
        public string DateSale { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Value { get; set; } = 0;
        public int Tax { get; set; } = 0;
        public int IdProperty { get; set; } = 0;
    }
}
