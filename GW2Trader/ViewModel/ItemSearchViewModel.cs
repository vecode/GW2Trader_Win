using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Command;
using GW2Trader.Data;
using GW2Trader.Model;
using GW2Trader.MVVM;

namespace GW2Trader.ViewModel
{
    public class ItemSearchViewModel : BaseViewModel
    {
        private readonly IApiDataUpdater _apiDataUpdater;
        private readonly WatchlistViewModel _watchlistViewModel;
        private readonly List<GameItemModel> _items;
        private readonly Dictionary<string, List<string>> _subTypeDictionary;

        public enum SortProperty
        {
            Name,
            BuyPrice,
            SellPrice,
            Margin,
            Demand,
            Supplay,
            Roi
        };

        public enum SortOrder
        {
            Ascending,
            Descending
        };

        public ItemSearchViewModel() { }

        public ItemSearchViewModel
            (
            List<GameItemModel> items,
            IApiDataUpdater apiDataUpdater,
            WatchlistViewModel watchlistViewModel
            )
        {
            ViewModelName = "Search";
            _apiDataUpdater = apiDataUpdater;
            _watchlistViewModel = watchlistViewModel;
            _items = items;

            Items = new PaginatedObservableCollection<GameItemModel>(_items, 20);

            _subTypeDictionary = BuildSubtypeDictionary(_items);
            SelectedRarity = RarityModel.Rarities.First();
        }

        public PaginatedObservableCollection<GameItemModel> Items { get; private set; }
        public List<RarityModel> Rarities { get { return RarityModel.Rarities; } }

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

        private RarityModel _selectedRarity;
        public RarityModel SelectedRarity
        {
            get
            {
                return _selectedRarity;                
            }
            set
            {
                _selectedRarity = value;
                RaisePropertyChanged("SelectedRarityIndex");
            }
        }

        private string _selectedType;
        public string SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                RaisePropertyChanged("SelectedType");
                SubTypes = _subTypeDictionary[SelectedType];
                SelectedSubType = SubTypes != null? SubTypes[0] : null;
            }
        }

        private string _selectedSubType;
        public string SelectedSubType
        {
            get
            {
                return _selectedSubType;
            }
            set
            {
                _selectedSubType = value;
                RaisePropertyChanged("SelectedSubType");
            }
        }

        public List<string> Types
        {
            get { return _subTypeDictionary.Keys.ToList(); }
        }

        private List<string> _subTypes;
        public List<string> SubTypes
        {
            get { return _subTypes; }
            set
            {
                _subTypes = value;
                RaisePropertyChanged("SubTypes");
            }
        }

        private SortProperty _selectedSortProperty;
        public SortProperty SelectedSortProperty
        {
            get
            {
                return _selectedSortProperty;
            }
            set
            {
                _selectedSortProperty = value;
                RaisePropertyChanged("SelectedSortProperty");
            }
        }

        private SortOrder _selectedSortOrder;

        public SortOrder SelectedSortOrder
        {
            get
            {
                return _selectedSortOrder;
            }
            set
            {
                _selectedSortOrder = value;
                RaisePropertyChanged("SelectedSortOrder");
            }
        }

        public List<string> SortProperties
        {
            get { return Enum.GetNames(typeof(SortProperty)).ToList(); }
        }

        public List<string> SortOrders
        {
            get { return Enum.GetNames(typeof (SortOrder)).ToList(); }
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

        public void UpdateCommerceData()
        {
            _apiDataUpdater.UpdateCommerceDataParallel(_items);
        }

        public void AddItemsToWatchlist(ItemWatchlistModel watchlist)
        {
            List<GameItemModel> itemsToAdd = SelectedItems.Cast<GameItemModel>().ToList();
            _watchlistViewModel.AddItemsToWatchlist(itemsToAdd, watchlist);
        }

        private Dictionary<string, List<string>> BuildSubtypeDictionary(List<GameItemModel> items)
        {
            var dictionary = new Dictionary<string, List<string>> {{"All", null}};

            foreach (string type in items.Select(i => i.Type).Distinct())
            {
                dictionary.Add(type, 
                    items.Where(i => i.Type == type).Select(t => t.SubType).Distinct().ToList());
                dictionary[type].Insert(0, "All");
            }
            return dictionary;
        }
    }
}