using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Data;
using GW2Trader.MVVM;
using System.Collections.ObjectModel;
using GW2Trader.Util;


namespace GW2Trader.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ITradingPostApiWrapper _tradingPostWrapper;
        private IItemRepository _dataRepository;
        private IApiDataUpdater _dataUpdater;
        private DbBuilder _dbBuilder;
        public Logger Logger { get; set; }

        private int _selectedTabIndex = 0;
        public int SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged("SelectedTabIndex");
            }
        }

        private ObservableCollection<BaseViewModel> _childViews;
        public ObservableCollection<BaseViewModel> ChildViews
        {
            get
            {
                return _childViews;
            }
        }

        public MainViewModel()
        {
            Logger = Logger.Instance;
            _tradingPostWrapper = new TradingPostApiWrapper(new ApiAccessor());
            _dataRepository = new ItemRepository(new GameDataContextProvider());
            _dataUpdater = new ApiDataUpdater(_tradingPostWrapper);
            _dbBuilder = new DbBuilder(_tradingPostWrapper, _dataRepository);

            if (_dataRepository.Items().Count() == 0)
            {
                _dbBuilder.BuildDatabase();                
            }
            Task.Run(() => _dbBuilder.LoadIcons());            
            
            _childViews = new ObservableCollection<BaseViewModel>();
            _childViews.Add(new ItemSearchViewModel(_dataRepository, _tradingPostWrapper, _dataUpdater, _dbBuilder));
            _childViews.Add(new InvestmentViewModel()); ;
        }
    }
}
