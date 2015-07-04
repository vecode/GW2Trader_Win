using System;
using SQLite.Net;
using SQLite.Net.Interop;
using DataLayer.Model;

namespace GW2Trader.Data.Db
{
    public class Database : SQLiteConnection
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="platform">Platform specific SQLite implementation</param>
        /// <param name="path">Path to database file</param>
        public Database(ISQLitePlatform platform, string path)
            : base(platform, path, true)
        {
            CreateTable<Item>();
            CreateTable<Investment>();
            CreateTable<InvestmentList>();
            CreateTable<Watchlist>();
            BusyTimeout = new TimeSpan(0,0,0,2);
        }
    }
}
