using DataLayer.Db;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public class WatchlistRepository : Repository<Watchlist>
    {
        public WatchlistRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }
    }
}
