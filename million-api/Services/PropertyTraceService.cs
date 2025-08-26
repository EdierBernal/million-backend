using Microsoft.Extensions.Options;

using million_api.Models.Constants;
using million_api.Models.Entities;

using MongoDB.Driver;

namespace million_api.Services
{
    public class PropertyTraceService
    {
        private readonly IMongoCollection<PropertyTrace> _PropertyTraceCollection;

        public PropertyTraceService(
            IOptions<DataBaseSettings> millionDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                millionDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                millionDatabaseSettings.Value.DatabaseName);

            _PropertyTraceCollection = mongoDatabase.GetCollection<PropertyTrace>(DataBaseCollections.PropertyTracesCollection);
        }

        public async Task<List<PropertyTrace>> GetAsync() =>
            await _PropertyTraceCollection.Find(_ => true).ToListAsync();

        public async Task<PropertyTrace?> GetAsync(string id) =>
            await _PropertyTraceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(PropertyTrace newPropertyTrace) =>
            await _PropertyTraceCollection.InsertOneAsync(newPropertyTrace);

        public async Task UpdateAsync(string id, PropertyTrace updatedPropertyTrace) =>
            await _PropertyTraceCollection.ReplaceOneAsync(x => x.Id == id, updatedPropertyTrace);

        public async Task RemoveAsync(string id) =>
            await _PropertyTraceCollection.DeleteOneAsync(x => x.Id == id);
    }
}
