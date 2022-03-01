using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> InsertAsyncGenericRepository(T Entity);
        public Task<List<T>> InsertListAsyncGenericRepository(List<T> EntityList);
        public Task<T> UpdateAsyncGenericRepository(T Entity);
        public Task<IEnumerable<T>> SelectListAsyncGenericRepository(Expression<Func<T, bool>> Where = null, Func<IQueryable<T>, IOrderedQueryable<T>> Orderby = null, string Includes = "");
        public Task<T> SelectAsyncGenericRepository(T Entity);
        public Task<int> DeleteAsyncGenericRepository(T Entity);
    }
}
