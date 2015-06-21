using System;
using DataLayer.Model;
using SQLite.Net;
using SQLite.Net.Interop;

namespace DataLayer.Db
{
    public class Database : SQLiteConnection
    {
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
