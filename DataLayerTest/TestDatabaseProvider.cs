using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GW2Trader.Data.Db;

namespace DataLayerTest
{
    internal class TestDatabaseProvider : IDatabaseProvider
    {
        private const string DatabasePath = "TestDb.sqlite";

        public TestDatabaseProvider()
        {
            if (System.IO.File.Exists(DatabasePath))
            {
                System.IO.File.Delete(DatabasePath);
            }
        }

        public Database GetDatabase()
        {
            return new Database(new SQLite.Net.Platform.Win32.SQLitePlatformWin32(), DatabasePath);
        }
    }
}
