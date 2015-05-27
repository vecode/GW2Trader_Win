using DataLayer.Db;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public class WatchlistRepository : GenericRepository<Watchlist>
    {
        public WatchlistRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }
    }
}
