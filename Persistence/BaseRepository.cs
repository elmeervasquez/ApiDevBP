using System.Reflection;
using Serilog;
using SQLite;

namespace ApiDevBP.Persistence
{
    public class BaseRepository
    {
        private readonly DbContext _dbContext;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<T> GetAll<T>() where T : class, new()
        {
            var table = (typeof(T).GetCustomAttribute(typeof(TableAttribute), true) as TableAttribute).Name;
            return _dbContext.GetConnection().Query<T>($"Select * from {table}");
        }

        public T GetById<T>(int id) where T : class, new()
        {
            var table = (typeof(T).GetCustomAttribute(typeof(TableAttribute), true) as TableAttribute).Name;
            return _dbContext.GetConnection().FindWithQuery<T>($"Select * from {table} where id = {id}");
        }

        public T Create<T>(T entity) where T : class, new()
        {
            try
            {
                _dbContext.GetConnection().Insert(entity);
            }
            catch (Exception e)
            {
                Log.Error($"DB on create error: {e.Message}");
            }
            return entity;
        }

        public T? Update<T>(T entity) where T : class, new()
        {
            try
            {
                _dbContext.GetConnection().Update(entity);

            }
            catch (Exception e)
            {
                Log.Error($"DB on update error: {e.Message}");
            }
            return entity;
        }

        public void Delete<T>(T entity)
        {
            try
            {
                _dbContext.GetConnection().Delete(entity);
            }
            catch (Exception e)
            {
                Log.Error($"DB on delete error: {e.Message}");
            }
        }

    }
}