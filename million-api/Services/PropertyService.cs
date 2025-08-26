using Microsoft.Extensions.Options;

using million_api.Models.Constants;
using million_api.Models.Entities;

using MongoDB.Driver;

namespace million_api.Services
{
    public class PropertyService
    {
        private readonly IMongoCollection<Property> _propertiesCollection;

        public PropertyService(
            IOptions<DataBaseSettings> millionDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                millionDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                millionDatabaseSettings.Value.DatabaseName);

            _propertiesCollection = mongoDatabase.GetCollection<Property>(DataBaseCollections.PropertiesCollection);
        }

        public async Task<List<Property>> GetAsync() =>
            await _propertiesCollection.Find(_ => true).ToListAsync();

        public async Task<Property?> GetAsync(string id) =>
            await _propertiesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Property newProperty) =>
            await _propertiesCollection.InsertOneAsync(newProperty);

        public async Task UpdateAsync(string id, Property updatedProperty) =>
            await _propertiesCollection.ReplaceOneAsync(x => x.Id == id, updatedProperty);

        public async Task RemoveAsync(string id) =>
            await _propertiesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
