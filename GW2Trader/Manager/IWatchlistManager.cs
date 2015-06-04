using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GW2Trader.Manager
{
    public interface IWatchlistManager
    {
        List<Watchlist> GetWatchlists();
        void AddWatchlist(string name, string description);
        void DeleteWatchlist(Watchlist watchlist);
        void UpdateWatchlist(Watchlist watchlist);
    }
}
