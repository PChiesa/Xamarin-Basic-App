using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BasicApp.Database
{
    public interface IBaseRepository<T> where T : IEntity
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);

        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        T GetOneByPredicate(Expression<Func<T, bool>> predicate);
        Task<T> GetOneByPredicateAsync(Expression<Func<T, bool>> predicate);

        List<T> GetListByPredicate(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListByPredicateAsync(Expression<Func<T, bool>> predicate);

        List<T> EnumerateAll();
        Task<List<T>> EnumerateAllAsync();

        Task AddAllAsync(IEnumerable<T> entities);
        Task RemoveAllAsync();

        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAllAsync(IEnumerable<T> entities);
    }
}
