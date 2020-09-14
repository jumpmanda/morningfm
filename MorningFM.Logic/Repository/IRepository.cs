using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MorningFM.Logic.Repositories
{
    public interface IRepository<T>
    {
        #region Get
        List<T> Get<T>(Expression<Func<T, bool>> filterFunc);
        Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> filterFunc);
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
        #endregion

        #region Add
        void Add<T>(T item);
        Task AddAsync<T>(T item);
        #endregion

        #region Update
        void Update<T>(string id, T item);
        Task UpdateAsync<T>(string id, T item);
        #endregion

        #region Delete
        void Delete<T>(Expression<Func<T, bool>> filterFunc); //todo make delete one or many               
        Task DeleteAsync<T>(Expression<Func<T, bool>> filterFunc);
        #endregion 
    }
}
