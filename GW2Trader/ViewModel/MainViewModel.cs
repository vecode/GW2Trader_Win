using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Data;
using GW2Trader.MVVM;
using System.Collections.ObjectModel;


namespace GW2Trader.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private IGameDataContext _dataContext;
        private ITradingPostApiWrapper _tradingPostWrapper;
        private IGameDataRepository _dataRepository;
        private IApiDataUpdater _dataUpdater;
        private DbBuilder _dbBuilder;

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
            _dataContext = new GameDataContext();
            _tradingPostWrapper = new TradingPostApiWrapper(new ApiAccessor());
            _dataRepository = new GameDataRepository(_dataContext);
            _dataUpdater = new ApiDataUpdater(_tradingPostWrapper);
            _dbBuilder = new DbBuilder(_tradingPostWrapper, _dataContext);

            if (_dataRepository.GetAllGameItems().Count() == 0)
                _dbBuilder.BuildDatabase();

            _childViews = new ObservableCollection<BaseViewModel>();
            _childViews.Add(new ItemOverviewViewModel(_dataRepository, _tradingPostWrapper, _dataUpdater, _dbBuilder));
            _childViews.Add(new InvestmentViewModel()); ;
        }
    }
}
