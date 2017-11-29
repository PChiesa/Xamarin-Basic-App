using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicApp.Database
{
    public interface IBaseRepository<T> where T : IEntity
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);

        T GetById(int id);
        T GetOneByPredicate(Func<T, bool> predicate);
        IEnumerable<T> GetEnumerableByPredicate(Func<T, bool> predicate);
        List<T> GetListByPredicate(Func<T, bool> predicate);

        IEnumerable<T> EnumerateAll();
        List<T> ListAll();

        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);


    }
}
