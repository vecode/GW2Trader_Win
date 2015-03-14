using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
        private int _selectedTabIndex;

        public MainViewModel()
        {
            IGameDataContextProvider contextProvider;
            if(System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                contextProvider = new FakeDataContextProvider();
            }
            else
            {
                contextProvider = new GameDataContextProvider();
            }

            Logger = Logger.Instance;
            ITradingPostApiWrapper tradingPostWrapper = new TradingPostApiWrapper(new ApiAccessor());
            IApiDataUpdater itemUpdater = new ApiDataUpdater(tradingPostWrapper);

            DbBuilder dbBuilder = new DbBuilder(tradingPostWrapper, contextProvider);
            dbBuilder.BuildDatabase();

            List<GameItemModel> items;
            using (var context = contextProvider.GetContext())
            {
                items = context.GameItems.ToList();
            }

            ChildViews = new ObservableCollection<BaseViewModel>
            {
                new ItemSearchViewModel(items, tradingPostWrapper, itemUpdater),
                new WatchlistViewModel(contextProvider, itemUpdater)
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