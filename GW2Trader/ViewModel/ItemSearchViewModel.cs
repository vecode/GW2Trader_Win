using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.MVVM;
using GW2Trader.Data;
using GW2Trader.Model;
using GW2Trader.Command;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;

namespace GW2Trader.ViewModel
{
    public class ItemSearchViewModel : BaseViewModel
    {
        private IGameDataRepository _dataRepository;
        private ITradingPostApiWrapper _tradingPostApiWrapper;
        private IApiDataUpdater _apiDataUpdater;
        private DbBuilder _dbBuilder;

        #region observable properties
        public ListCollectionView Items { get; set; }
        public ObservableCollection<GameItemModel> SelectedItems { get; private set; }

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

        private int _minLvl = 0;
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

        private int _minMargin = 0;
        public int MinMargin
        {
            get { return _minMargin; }
            set
            {
                _minMargin = value;
                RaisePropertyChanged("MinMargin");
            }
        }

        private int _maxMargin = 0;
        public int MaxMargin
        {
            get { return _maxMargin; }
            set
            {
                _maxMargin = value;
                RaisePropertyChanged("MaxMargin");
            }
        }

        private int _minROI = 0;
        public int MinROI
        {
            get { return _minROI; }
            set
            {
                _minROI = value;
                RaisePropertyChanged("MinROI");
            }
        }

        private int _maxROI = 0;
        public int MaxROI
        {
            get { return _maxROI; }
            set
            {
                _maxROI = value;
                RaisePropertyChanged("MaxROI");
            }
        }
        #endregion

        #region Commands
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new ItemSearchCommand.SearchCommand();
                return _searchCommand;
            }
            set { _searchCommand = value; }
        }

        private RelayCommand _searchResetCommand;
        public RelayCommand SearchResetCommand
        {
            get
            {
                if (_searchResetCommand == null)
                    _searchResetCommand = new ItemSearchCommand.SearchResetCommand();
                return _searchResetCommand;
            }
            set { _searchCommand = value; }        
        }
        #endregion

        public ItemSearchViewModel() { }

        public ItemSearchViewModel(
            IGameDataRepository dataRepository,
            ITradingPostApiWrapper tradingPostApiWrapper,
            IApiDataUpdater apiDataUpdater,
            DbBuilder dbBuilder)
        {
            _dataRepository = dataRepository;
            _tradingPostApiWrapper = tradingPostApiWrapper;
            _apiDataUpdater = apiDataUpdater;
            _dbBuilder = dbBuilder;       
                
            if (_dataRepository.GetAllGameItems().Count() == 0)
                _dbBuilder.BuildDatabase();
            
            Items = new ListCollectionView(_dataRepository.GetAllGameItems().ToList());
        }
    }
}
