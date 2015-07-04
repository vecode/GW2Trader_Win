using System.IO;
using Android.OS;
using GW2Trader.Data.Db;
using SQLite.Net.Platform.XamarinAndroid;

namespace GW2Trader.Android.Util
{
    public class DbProvider : IDatabaseProvider
    {
        public Database GetDatabase()
        {
            string folder = Environment.ExternalStorageDirectory.ToString();
            string appFolder = "GW2Trader";
            string databaseFile = "db.sqlite";
            string dbPath = Path.Combine(folder, appFolder, databaseFile);

            Directory.CreateDirectory(Path.Combine(folder, appFolder));

            return new Database(new SQLitePlatformAndroid(), dbPath);
        }
    }
}