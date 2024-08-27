using ApiDevBP.Configuration;
using Microsoft.Extensions.Options;
using SQLite;

namespace ApiDevBP.Persistence
{
    public class DbContext
    {
        private readonly SQLiteConnection _db;

        public DbContext(IOptions<DbSettings> dbSettings)
        {
            _db = new SQLiteConnection(dbSettings.Value.DbPath);
        }

        public SQLiteConnection GetConnection()
        {
            return _db;
        }
    }

}