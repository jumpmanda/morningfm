using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MorningFM.Logic.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private string _collection; 
        private MongoRepository _mongoRepo; 
        
        public BaseRepository(string connection, string databaseName, string collection)
        {
            if (string.IsNullOrEmpty(collection))
            {
                throw new Exception("Collection was not provided."); 
            }
            _mongoRepo = new MongoRepository(connection, databaseName);
            _collection = collection; 
        }

        public virtual void Add<T>(T item)
        {
            _mongoRepo.Insert<T>(_collection, item);
        }

        public async Task AddAsync<T>(T item)
        {
            await _mongoRepo.InsertAsync<T>(_collection, item); 
        }

        public virtual void Delete<T>(Expression<Func<T, bool>> filterFunc)
        {
            _mongoRepo.Delete(_collection, filterFunc);
        }

        public virtual async Task DeleteAsync<T>(Expression<Func<T, bool>> filterFunc)
        {
            await _mongoRepo.DeleteAsync<T>(_collection, filterFunc);
        }

        public virtual List<T> Get<T>(Expression<Func<T, bool>> filterFunc)
        {
            return _mongoRepo.FindMany(_collection, filterFunc);
        }

        public virtual async Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> filterFunc)
        {
           return await _mongoRepo.FindManyAsync<T>(_collection, filterFunc);
        }

        public virtual T GetById(string id)
        {
            return _mongoRepo.FindById<T>(_collection, id);
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _mongoRepo.FindByIdAsync<T>(_collection, id);
        }

        public void Update<T>(string id, T item)
        {
            _mongoRepo.Update(_collection, id, item);
        }

        public async Task UpdateAsync<T>(string id, T item)
        {
            await _mongoRepo.UpdateAsync(_collection, id, item);
        }
    }
}
