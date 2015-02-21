using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using GW2TPApiWrapper;
using GW2TPApiWrapper.Wrapper;

namespace GW2Trader.Data
{
    public interface IGameDataRepository
    {
        ObservableCollection<InvestmentWatchlistModel> InvestmentLists { get; }
        ObservableCollection<ItemWatchlistModel> ItemWatchlists {get;}

        GameItemModel GameItemById(int id);
        IEnumerable<GameItemModel> GameItemsById(int[] ids);
        IEnumerable<GameItemModel> GetAllGameItems();
       
        void AddWatchlist(InvestmentWatchlistModel watchlist);
        void AddWatchlist(ItemWatchlistModel watchlist);
        void AddItemToWatchlist(ItemWatchlistModel watchlist, GameItemModel item);
        void AddInvestmentToWatchlist(InvestmentWatchlistModel watchlist, InvestmentModel investment);

        void DeleteWatchlist<T>(WatchlistModel<T> watchlist) where T : WatchlistModel<T>;
        void DeleteItemFromWatchlist<T>(WatchlistModel<T> watchlist, T item);

        void RebuiltGameItemDatabase(ITradingPostApiWrapper tpApiWrapper);
    }
}
