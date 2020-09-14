using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MorningFM.Logic.Repositories
{
    public class MongoRepository
    {
        private MongoClient _client;
        public IMongoDatabase db;

        public MongoRepository(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString) ?? throw new NullReferenceException("Mongo client is null.");
            db = _client.GetDatabase(dbName) ?? throw new NullReferenceException("Database could not be retrieved.");

        }

        public IMongoDatabase GetDatabase(string dbName)
        {
            //TODO: Do some validation on name
            return _client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(IMongoDatabase db, string collectionName)
        {
            return db.GetCollection<T>(collectionName);
        }

        #region Insert
        public void Insert<T>(string collection, T item)
        {
            var dbCollection = db.GetCollection<T>(collection);
            dbCollection.InsertOne(item);
        }

        public async Task InsertAsync<T>(string collection, T item)
        {
            var dbCollection = db.GetCollection<T>(collection);
            await dbCollection.InsertOneAsync(item);
        }
        #endregion

        #region Query
        public List<T> FindAll<T>(string collection)
        {
            var dbCollection = db.GetCollection<T>(collection);
            var results = dbCollection.Find(new BsonDocument());
            return results.ToList<T>();
        }

        public List<T> FindMany<T>(string collection, Expression<Func<T, bool>> filterFunc)
        {
            var dbCollection = db.GetCollection<T>(collection);
            var results = dbCollection.Find(filterFunc);
            return results.ToList<T>();
        }

        public async Task<List<T>> FindManyAsync<T>(string collection, Expression<Func<T, bool>> filterFunc)
        {
            var dbCollection = db.GetCollection<T>(collection);
            var results = await dbCollection.FindAsync(filterFunc);
            return results.ToList<T>();
        }

        public T FindById<T>(string collection, string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var dbCollection = db.GetCollection<T>(collection);
            return dbCollection.Find<T>(filter).FirstOrDefault();
        }

        public async Task<T> FindByIdAsync<T>(string collection, string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var dbCollection = db.GetCollection<T>(collection);
            var result = await dbCollection.FindAsync<T>(filter);
            return result.FirstOrDefault();
        }
        #endregion

        #region Update 

        /// <summary>
        /// Update entire document contents by finding document id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="item"></param>
        public void Update<T>(string collection, Expression<Func<T, bool>> filterFunc, T item)
        {
            var dbCollection = db.GetCollection<T>(collection);
            var builder = new FilterDefinitionBuilder<T>();
            var filter = builder.Where(filterFunc);
            dbCollection.ReplaceOne(filter, item);
        }

        public async Task UpdateAsync<T>(string collection, Expression<Func<T, bool>> filterFunc, T item)
        {
            var dbCollection = db.GetCollection<T>(collection);
            var builder = new FilterDefinitionBuilder<T>();
            var filter = builder.Where(filterFunc);
            await dbCollection.ReplaceOneAsync(filter, item);
        }

        public void Update<T>(string collection, string id, T item)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var dbCollection = db.GetCollection<T>(collection);
            dbCollection.ReplaceOne(filter, item);
        }

        public async Task UpdateAsync<T>(string collection, string id, T item)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var dbCollection = db.GetCollection<T>(collection);
            await dbCollection.ReplaceOneAsync(filter, item);
        }
        #endregion

        #region Delete 
        /// <summary>
        /// Delete a single document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="filterFunc"></param>
        public void Delete<T>(string collection, Expression<Func<T, bool>> filterFunc)
        {
            var dbCollection = db.GetCollection<T>(collection);
            dbCollection.DeleteOne<T>(filterFunc);
        }

        public async Task DeleteAsync<T>(string collection, Expression<Func<T, bool>> filterFunc)
        {
            var dbCollection = db.GetCollection<T>(collection);
            await dbCollection.DeleteOneAsync<T>(filterFunc);
        }

        /// <summary>
        /// Delete a single document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="filterFunc"></param>
        public void DeleteMany<T>(string collection, Expression<Func<T, bool>> filterFunc)
        {
            var dbCollection = db.GetCollection<T>(collection);
            dbCollection.DeleteMany<T>(filterFunc);
        }

        #endregion

    }
}
