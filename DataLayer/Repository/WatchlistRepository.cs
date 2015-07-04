using DataLayer.Model;
using GW2Trader.Data.Db;

namespace DataLayer.Repository
{
    public class WatchlistRepository : Repository<Watchlist>
    {
        public WatchlistRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }
    }
}
