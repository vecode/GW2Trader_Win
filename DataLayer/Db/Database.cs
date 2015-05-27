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
            CreateTable<Icon>();
            CreateTable<Investment>();
            CreateTable<InvestmentList>();
            CreateTable<Watchlist>();
        }
    }
}
