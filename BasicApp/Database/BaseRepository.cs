using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;

namespace BasicApp.Database
{
    public class BaseRepository<T> : IBaseRepository<T> where T : IEntity, new()
    {
        private readonly ISQLite _sqlite;

        private SQLiteConnection _connection;
        private SQLiteAsyncConnection _asyncConnection;

        public BaseRepository(ISQLite sqlite)
        {
            _sqlite = sqlite;
        }

        public void OpenConnection()
        {
            _connection = _sqlite.GetConnection();
        }

        public async Task OpenAsyncConnection()
        {
            _asyncConnection = _sqlite.GetAsyncConnection();

            try
            {
                await _asyncConnection.Table<T>().CountAsync();
            }
            catch (SQLiteException)
            {
                await _asyncConnection.CreateTableAsync<T>();
            }


        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public async Task CloseAsyncConnection()
        {
            await Task.Run(() =>
            {
                _asyncConnection.GetConnection().Close();
            });
        }

        public void Add(T entity)
        {
            _connection.Insert(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _asyncConnection.InsertAsync(entity);
        }

        public List<T> EnumerateAll()
        {
            return _connection.Table<T>().ToList();
        }

        public async Task<List<T>> EnumerateAllAsync()
        {
            return await _asyncConnection.Table<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            return _connection.Find<T>(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _asyncConnection.FindAsync<T>(id);
        }

        public List<T> GetListByPredicate(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T GetOneByPredicate(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOneByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }


    }
}
