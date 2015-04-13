using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Service
{
    public interface IDataService
    {
        IEnumerable<GameItemModel> Items { get; }
        IEnumerable<ItemWatchlistModel> ItemWatchlists { get; }
        IEnumerable<InvestmentWatchlistModel> InvestmentWatchlists { get; }

        void Add(GameItemModel item);
        void Add(InvestmentModel investment);
        void Add(InvestmentWatchlistModel watchlist);
        void Add(ItemWatchlistModel watchlist);

        void Update(GameItemModel item);
        void Update(InvestmentModel investment);
        void Update(InvestmentWatchlistModel watchlist);
        void Update(ItemWatchlistModel watchlist);

        void Delete(GameItemModel item);
        void Delete(InvestmentModel investment);
        void Delete(InvestmentWatchlistModel watchlist);
        void Delete(ItemWatchlistModel watchlist);

        void Save();
    }
}
