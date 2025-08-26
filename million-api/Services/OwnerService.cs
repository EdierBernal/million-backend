using Microsoft.Extensions.Options;

using million_api.Models.Constants;
using million_api.Models.Entities;

using MongoDB.Driver;

namespace million_api.Services
{
    public class OwnerService
    {
        private readonly IMongoCollection<Owner> _ownersCollection;

        public OwnerService(
            IOptions<DataBaseSettings> millionDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                millionDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                millionDatabaseSettings.Value.DatabaseName);

            _ownersCollection = mongoDatabase.GetCollection<Owner>(DataBaseCollections.OwnersCollection);
        }

        public async Task<List<Owner>> GetAsync() =>
            await _ownersCollection.Find(_ => true).ToListAsync();

        public async Task<Owner?> GetAsync(string id) =>
            await _ownersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Owner newOwner) =>
            await _ownersCollection.InsertOneAsync(newOwner);

        public async Task UpdateAsync(string id, Owner updatedOwner) =>
            await _ownersCollection.ReplaceOneAsync(x => x.Id == id, updatedOwner);

        public async Task RemoveAsync(string id) =>
            await _ownersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
