using Shared;

namespace GW2Trader.Data.Db
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public Database GetDatabase()
        {
            return  new Database(AppSettings.SQLitePlatform, AppSettings.DatabaseFilePath);
        }
    }
}
