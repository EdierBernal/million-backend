using Microsoft.Extensions.Options;

using million_api.Models.Constants;
using million_api.Models.Entities;

using MongoDB.Driver;

namespace million_api.Services
{
    public class PropertyImageService
    {
        private readonly IMongoCollection<PropertyImage> _propertyImageCollection;

        public PropertyImageService(
            IOptions<DataBaseSettings> millionDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                millionDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                millionDatabaseSettings.Value.DatabaseName);

            _propertyImageCollection = mongoDatabase.GetCollection<PropertyImage>(DataBaseCollections.PropertyImagesCollection);
        }

        public async Task<List<PropertyImage>> GetAsync() =>
            await _propertyImageCollection.Find(_ => true).ToListAsync();

        public async Task<PropertyImage?> GetAsync(string id) =>
            await _propertyImageCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(PropertyImage newPropertyImage) =>
            await _propertyImageCollection.InsertOneAsync(newPropertyImage);

        public async Task UpdateAsync(string id, PropertyImage updatedPropertyImage) =>
            await _propertyImageCollection.ReplaceOneAsync(x => x.Id == id, updatedPropertyImage);

        public async Task RemoveAsync(string id) =>
            await _propertyImageCollection.DeleteOneAsync(x => x.Id == id);
    }
}
