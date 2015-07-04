using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GW2Trader.Data.Db;

namespace GW2TraderTest
{
    internal class TestDatabaseProvider : IDatabaseProvider
    {
        private const string DatabasePath = "TestDb.sqlite";

        public Database GetDatabase()
        {
            return new Database(new SQLite.Net.Platform.Win32.SQLitePlatformWin32(), DatabasePath);
        }

        public static void DeleteDb()
        {
            if (System.IO.File.Exists(DatabasePath))
            {
                System.IO.File.Delete(DatabasePath);
            }
        }
    }
}
