using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OzeContract.Databases;

namespace OzeDatabase.Context
{
    public class MongoContext : OzeContract.Databases.IMongoContext
    {
        private readonly MongoClient client;
        private string database;
        private string collection;

        public MongoContext(string url)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(url));

            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            client = new MongoClient(settings);
        }

        private IMongoCollection<T> GetCollection<T>() => client.GetDatabase(database).GetCollection<T>(collection);

        public async Task<List<T>> GetAll<T>()
        {
            var response = new List<T>();

            try
            {
                response = await GetCollection<T>().AsQueryable().ToListAsync();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return response;
        }

        public IMongoContext Set(string database, string collection)
        {
            this.database = database;
            this.collection = collection;

            return this;
        }

        public async Task Add<T>(T item)
        {
            try
            {
                await GetCollection<T>().InsertOneAsync(item);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<T> GetSingle<T>(string objectId) where T : new()
        {
            T result = new T();

            try
            {
                var filter = GetFilter<T>(objectId);
                var queryResult = await GetCollection<T>().FindAsync(filter);

                result = await queryResult.FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        private FilterDefinition<T> GetFilter<T>(string objectId)
        {
            var id = ObjectId.Parse(objectId);
            var builder = Builders<T>.Filter;
            var filter = builder.Eq("_id", id);

            return filter;
        }

        public async Task Remove<T>(string objectId)
        {
            try
            {
                await GetCollection<T>().FindOneAndDeleteAsync(GetFilter<T>(objectId));
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task Update<T>(string objectId, T item)
        {
            try
            {
                await GetCollection<T>().FindOneAndReplaceAsync<T>(GetFilter<T>(objectId), item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}