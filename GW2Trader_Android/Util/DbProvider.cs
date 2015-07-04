using System.IO;

using GW2Trader.Data.Db;

namespace GW2Trader_Android.Util
{
    public class DbProvider : IDatabaseProvider
    {
        public Database GetDatabase()
        {
            string folder = Android.OS.Environment.ExternalStorageDirectory.ToString();
            string appFolder = "GW2Trader";
            string databaseFile = "db.sqlite";
            string dbPath = Path.Combine(folder, appFolder, databaseFile);

            Directory.CreateDirectory(Path.Combine(folder, appFolder));
         
            return new Database(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbPath);
        }
    }
}