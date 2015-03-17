using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Command;
using GW2Trader.Data;
using GW2Trader.Model;
using GW2Trader.MVVM;
using System.Windows.Controls;
using System.Windows.Data;
using GW2Trader.Control;

namespace GW2Trader.ViewModel
{
    public class ItemSearchViewModel : BaseViewModel
    {
        private readonly IApiDataUpdater _apiDataUpdater;
        private readonly WatchlistViewModel _watchlistViewModel;

        public ItemSearchViewModel() {}

        public ItemSearchViewModel
            (
            List<GameItemModel> items,
            ITradingPostApiWrapper tradingPostApiWrapper,
            IApiDataUpdater apiDataUpdater,
            WatchlistViewModel watchlistViewModel
            )
        {
            ViewModelName = "Search";
            _apiDataUpdater = apiDataUpdater;
            _watchlistViewModel = watchlistViewModel;

            Items = new PaginatedObservableCollection<GameItemModel>(items, 20);
            Task.Run(() => UpdateCommerceData());
        }

        public void UpdateCommerceData()
        {
            _apiDataUpdater.UpdateCommerceData(Items);
        }

        #region observable properties

        private IList _selectedItems;
        public IList SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged("SelectedItems");
            }
        }

        public PaginatedObservableCollection<GameItemModel> Items { get; private set; }

        private string _keyword = string.Empty;

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }

        private int _minLvl;

        public int MinLvl
        {
            get { return _minLvl; }
            set
            {
                _minLvl = value;
                RaisePropertyChanged("MinLvl");
            }
        }

        private int _maxLvl = 80;

        public int MaxLvl
        {
            get { return _maxLvl; }
            set
            {
                _maxLvl = value;
                RaisePropertyChanged("MaxLvl");
            }
        }

        private int _minMargin;

        public int MinMargin
        {
            get { return _minMargin; }
            set
            {
                _minMargin = value;
                RaisePropertyChanged("MinMargin");
            }
        }

        private int _maxMargin;

        public int MaxMargin
        {
            get { return _maxMargin; }
            set
            {
                _maxMargin = value;
                RaisePropertyChanged("MaxMargin");
            }
        }

        private int _minROI;

        public int MinROI
        {
            get { return _minROI; }
            set
            {
                _minROI = value;
                RaisePropertyChanged("MinROI");
            }
        }

        private int _maxROI;

        public int MaxROI
        {
            get { return _maxROI; }
            set
            {
                _maxROI = value;
                RaisePropertyChanged("MaxROI");
            }
        }

        private ItemWatchlistModel _selectedWatchlist;

        public ItemWatchlistModel SelectedWatchlist
        {
            get { return _selectedWatchlist; }
            set
            {
                _selectedWatchlist = value;
                RaisePropertyChanged("SelectedWatchlist");
            }
        }

        public string PageInfo
        {
            get { return Items.CurrentPage + "/" + Items.PageCount; }
        }

        public ObservableCollection<ItemWatchlistModel> Watchlists
        {
            get { return _watchlistViewModel.Watchlists; }
        }

        #endregion

        #region Commands

        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new SearchCommand()); }
            private set { _searchCommand = value; }
        }

        private RelayCommand _searchResetCommand;

        public RelayCommand SearchResetCommand
        {
            get { return _searchResetCommand ?? (_searchResetCommand = new SearchResetCommand()); }
            private set { _searchCommand = value; }
        }

        private RelayCommand _moveToNextPageCommand;

        public RelayCommand MoveToNextPageCommand
        {
            get { return _moveToNextPageCommand ?? (_moveToNextPageCommand = new MoveToNextPageCommand()); }
            private set { _moveToNextPageCommand = value; }
        }

        private RelayCommand _moveToPreviousCommand;

        public RelayCommand MoveToPreviousPageCommand
        {
            get { return _moveToPreviousCommand ?? (_moveToPreviousCommand = new MoveToPreviousPageCommand()); }
            private set { _moveToPreviousCommand = value; }
        }

        private RelayCommand _addItemsToWatchlistCommand;

        public RelayCommand AddItemsToWatchlistCommand
        {
            get
            {
                return _addItemsToWatchlistCommand ?? (_addItemsToWatchlistCommand = new AddItemsToWatchlistCommand());
            }
            private set { _addItemsToWatchlistCommand = value; }
        }
        #endregion

        public void AddItemsToWatchlist(ItemWatchlistModel watchlist)
        {
            List<GameItemModel> itemsToAdd = SelectedItems.Cast<GameItemModel>().ToList();
            _watchlistViewModel.AddItemsToWatchlist(itemsToAdd, watchlist);
        }
    }
}