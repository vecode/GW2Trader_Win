using DataLayer.Repository;
using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Manager
{
    public class WatchlistManager : IWatchlistManager
    {
        private WatchlistRepository _repository;

        public WatchlistManager(WatchlistRepository repository)
        {
            _repository = repository;
        }

        public List<Model.Watchlist> GetWatchlists()
        {
            return _repository.GetAll().Cast<Model.Watchlist>().ToList();
        }

        public void AddWatchlist(string name, string description)
        {
            _repository.Save(new DataLayer.Model.Watchlist 
            {
                Name = name, 
                Description = description
            });
        }

        public void DeleteWatchlist(Model.Watchlist watchlist)
        {
            _repository.Delete(watchlist);
        }

        public void UpdateWatchlist(Model.Watchlist watchlist)
        {
            _repository.Save(watchlist);
        }
    }
}
