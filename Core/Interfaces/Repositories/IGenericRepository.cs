using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class, IPrimaryKey
    {
        IQueryable<T> Query();
        Task AddAsync(T entity);
        Task<T> InsertAndReturnAsync(T entity);
        void Update(T entity);
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        void Remove(int id);
    }
}
