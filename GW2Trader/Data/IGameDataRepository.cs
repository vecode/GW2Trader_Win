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
        ObservableCollection<ItemIdWatchlistModel> ItemWatchlists { get; }
        GameItemModel GameItemById(int id);
        IEnumerable<GameItemModel> GameItemsById(int[] ids);
        IEnumerable<GameItemModel> GetAllGameItems();

        void AddWatchlist<T>(WatchlistModel<T> watchlist);
        void AddItemToWatchlist<T>(WatchlistModel<T> watchlist, T item);

        void DeleteWatchlist<T>(WatchlistModel<T> watchlist) where T : WatchlistModel<T>;
        void DeleteItemFromWatchlist<T>(WatchlistModel<T> watchlist, T item);

        void UpdateWatchlist<T>(WatchlistModel<T> watchlist);
        void UpdateWatchlistItem<T>(WatchlistModel<T> watchlist, T item);

        void RebuiltGameItemDatabase(ITradingPostApiWrapper tpApiWrapper);
    }
}
