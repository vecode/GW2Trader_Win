using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return new Database(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), "");
        }
    }
}