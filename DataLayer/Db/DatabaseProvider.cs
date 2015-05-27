using Shared;

namespace DataLayer.Db
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public Database GetDatabase()
        {
            return  new Database(AppSettings.SQLitePlatform, AppSettings.DatabaseFilePath);
        }
    }
}
