using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DataLayer.Db;

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