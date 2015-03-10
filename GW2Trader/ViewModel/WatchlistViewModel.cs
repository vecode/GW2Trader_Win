using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GW2Trader.Data;
using GW2Trader.Model;

namespace GW2Trader.ViewModel
{
    public class WatchlistViewModel : BaseViewModel
    {
        private IApiDataUpdater _apiDataUpdater;
        private IGameDataContextProvider _contextProvider;
        private IList<GameItemModel> _items;
        private ItemWatchlistModel _selectedWatchlist;
        private ObservableCollection<ItemWatchlistModel> _watchlists;

        public WatchlistViewModel(IGameDataContextProvider contextProvider, IList<GameItemModel> items,
            IApiDataUpdater apiDataUpdater)
        {
            ViewModelName = "Watchlists";
            _contextProvider = contextProvider;
            _apiDataUpdater = apiDataUpdater;
            _items = items;

            //using (var context = contextProvider.GetContext())
            //{
            //    Watchlists = new ObservableCollection<ItemWatchlistModel>(BuildWatchlists(context.ItemIdWatchlists.ToList(), items));
            //}
            Watchlists = new ObservableCollection<ItemWatchlistModel>
            {
                new ItemWatchlistModel
                {
                    Name = "Skins",
                    Description = "Some Skins",
                    Items = items.Where(i => i.Name.ToLower().Contains("skin)")).ToList()
                },
                new ItemWatchlistModel
                {
                    Name = "dyes",
                    Description = "Some dyes",
                    Items = items.Where(i => i.Name.ToLower().Contains("dye)")).ToList()
                }
            };

            if (Watchlists.Count != 0)
            {
                SelectedWatchlist = Watchlists[0];
            }
        }

        public ObservableCollection<ItemWatchlistModel> Watchlists
        {
            get { return _watchlists; }
            set
            {
                _watchlists = value;
                RaisePropertyChanged("WatchLists");
            }
        }

        public ItemWatchlistModel SelectedWatchlist
        {
            get { return _selectedWatchlist; }
            set
            {
                _selectedWatchlist = value;
                RaisePropertyChanged("SelectedWatchlist");
            }
        }

        private IList<ItemWatchlistModel> BuildWatchlists(IList<ItemIdWatchlistModel> itemIdWatchlists,
            IList<GameItemModel> items)
        {
            return itemIdWatchlists.Select(itemIdWatchlist => BuildWatchlist(itemIdWatchlist, items)).ToList();
        }

        private ItemWatchlistModel BuildWatchlist(ItemIdWatchlistModel itemIdWatchlist, IList<GameItemModel> items)
        {
            var itemWatchlist = new ItemWatchlistModel();
            foreach (var id in itemIdWatchlist.Items)
            {
                itemWatchlist.Items.Add(items.Single(i => i.ItemId == id));
            }
            return itemWatchlist;
        }
    }
}