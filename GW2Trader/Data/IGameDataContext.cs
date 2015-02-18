using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public interface IGameDataContext
    {
        IDbSet<GameItemModel> GameItems { get; }
        IDbSet<Watchlist<InvestmentModel>> Investments { get; }
        IDbSet<Watchlist<int>> WatchedItemIds { get; }
    }
}
