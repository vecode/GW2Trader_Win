using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader.Desktop.Command;
using GW2Trader.Desktop.Data;
using GW2Trader.Desktop.DesignTimeErrorPrevention;
using GW2Trader.Desktop.Model;
using GW2Trader.Desktop.Util;

namespace GW2Trader.Desktop.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private int _selectedTabIndex;
        private IGameDataContextProvider _contextProvider = new GameDataContextProvider();
        private List<GameItemModel> _sharedItems;
        private Dictionary<int, GameItemModel> _sharedItemDictionary;
        private ITradingPostApiWrapper _tpApiWrapper;
        private IApiDataUpdater _dataUpdater;
        private IDbBuilder _dbBuilder;

        public ObservableCollection<BaseViewModel> ChildViews { get; private set; }

        public MainViewModel()
        {
            Init();

            var watchlistViewModel = new WatchlistViewModel(_contextProvider, _sharedItems);
            var searchViewModel = new ItemSearchViewModel(_sharedItems, _dataUpdater, watchlistViewModel);
            var investmentViewModel = new InvestmentViewModel(_contextProvider, _sharedItems, _sharedItemDictionary);
            var settingsViewModel = new SettingsViewModel(_dbBuilder);

            ChildViews = new ObservableCollection<BaseViewModel>
            {
                searchViewModel,
                watchlistViewModel,
                investmentViewModel,
                settingsViewModel
            };
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged("SelectedTabIndex");
            }
        }

        private void Init()
        {
            // prevent design time error in xaml designer
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                _contextProvider = new FakeDataContextProvider();
            }
            else
            {
                _contextProvider = new GameDataContextProvider();
            }

            _tpApiWrapper = new TradingPostApiWrapper(new ApiAccessor(new WebClientProvider()));
            _dataUpdater = new ApiDataUpdater(_tpApiWrapper);

            _dbBuilder = new DbBuilder(_tpApiWrapper, _contextProvider);
            _dbBuilder.BuildDatabase();
            
            using (var context = _contextProvider.GetContext())
            {
                _sharedItems = context.GameItems.ToList();
            }
            _sharedItemDictionary = _sharedItems.ToDictionary(item => item.ItemId, item => item);
            Task.Run(() => _dataUpdater.UpdatePricesParallel(_sharedItems));
        }

        public void UpdateCommerceDataOfAllItems()
        {
            _dataUpdater.UpdatePricesParallel(_sharedItems);
        }

        public void UpdateCommerceDataOfShownItems()
        {
            BaseViewModel currentViewModel = ChildViews[SelectedTabIndex];
            IItemViewer model = currentViewModel as IItemViewer;
            if (model != null)
            {
                _dataUpdater.UpdatePricesParallel(model.ShownItems);
            }
        }

        private RelayCommand _updateAllItemsCommand;

        public RelayCommand UpdateAllItemsCommand
        {
            get
            {
                if (_updateAllItemsCommand == null)
                    _updateAllItemsCommand = new UpdateAllItemsCommand();
                return _updateAllItemsCommand;
            }
        }

        private RelayCommand _updateCurrentItemsCommand;

        public RelayCommand UpdateCurrentItemsCommand
        {
            get
            {
                if (_updateCurrentItemsCommand == null)
                    _updateCurrentItemsCommand = new UpdateCurrentItemsCommand();
                return _updateCurrentItemsCommand;
            }
        }
    }
}