using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;
using Prism.Events;
using BasicApp.UI.PubSubEvents;

namespace BasicApp.Database
{
    public class BaseRepository<T> : IBaseRepository<T> where T : IEntity, new()
    {
        private readonly SQLiteConnection _connection;
        private readonly SQLiteAsyncConnection _asyncConnection;

        private readonly IEventAggregator _eventAggregator;

        public BaseRepository(ISQLite sqlite, IEventAggregator eventAggregator)
        {
            _connection = sqlite.GetConnection();
            _asyncConnection = sqlite.GetAsyncConnection();
            _eventAggregator = eventAggregator;

            CreateTableIfNonExistent();

            _eventAggregator.GetEvent<LogoutEvent>().Subscribe(async () => await RemoveAllAsync());
        }

        ~BaseRepository()
        {
            _connection.Close();
            _asyncConnection.GetConnection().Close();
        }

        private void CreateTableIfNonExistent()
        {
            try
            {
                _connection.Table<T>().Count();
            }
            catch (SQLiteException)
            {
                _connection.CreateTable<T>();
            }
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

        public async Task<List<T>> GetListByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            return await _asyncConnection.Table<T>().Where(predicate).ToListAsync();
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

        public async Task AddAllAsync(IEnumerable<T> entities)
        {
            for (int i = 0; i < entities.Count(); i++)
            {
                await AddAsync(entities.ElementAt(i));
            }
        }

        public async Task RemoveAllAsync()
        {
            await _asyncConnection.DropTableAsync<T>();
            await _asyncConnection.CreateTableAsync<T>();
        }
    }
}
