using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Data;
using GW2Trader.DesignTimeErrorPrevention;
using GW2Trader.Model;
using GW2Trader.Util;

namespace GW2Trader.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IGameDataContextProvider _contextProvider;
        private readonly DbBuilder _dbBuilder;
        private readonly List<GameItemModel> _items;
        private readonly IApiDataUpdater _itemUpdater;
        private readonly ITradingPostApiWrapper _tradingPostWrapper;
        private int _selectedTabIndex;

        public MainViewModel()
        {
            if(System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                _contextProvider = new FakeDataContextProvider();
            }
            else
            {
                _contextProvider = new GameDataContextProvider();
            }

            Logger = Logger.Instance;
            _tradingPostWrapper = new TradingPostApiWrapper(new ApiAccessor());
            _itemUpdater = new ApiDataUpdater(_tradingPostWrapper);
            _dbBuilder = new DbBuilder(_tradingPostWrapper, _contextProvider);

            _dbBuilder.BuildDatabase();

            using (var context = _contextProvider.GetContext())
            {
                _items = context.GameItems.ToList();
            }

            ChildViews = new ObservableCollection<BaseViewModel>
            {
                new ItemSearchViewModel(_items, _tradingPostWrapper, _itemUpdater, _dbBuilder),
                new WatchlistViewModel(_contextProvider, _items, _itemUpdater)
            };
        }

        public Logger Logger { get; set; }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged("SelectedTabIndex");
            }
        }

        public ObservableCollection<BaseViewModel> ChildViews { get; private set; }
    }
}