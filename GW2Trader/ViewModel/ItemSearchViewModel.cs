using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private GameDataContextProvider _contextProvider;
        private static object _lock = new object();

        public ItemSearchViewModel()
        {
        }

        public ItemSearchViewModel(
            List<GameItemModel> items,
            ITradingPostApiWrapper tradingPostApiWrapper,
            IApiDataUpdater apiDataUpdater)
        {
            ViewModelName = "Search";
            _apiDataUpdater = apiDataUpdater;

            Items = new PaginatedObservableCollection<GameItemModel>(items, 10);
            Task.Run(() => UpdateCommerceData());

            SelectedItems = new ArrayList();
            //BindingOperations.EnableCollectionSynchronization();
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
                if (_selectedItems != null) Console.WriteLine(_selectedItems.Count);
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

        public string PageInfo
        {
            get { return Items.CurrentPage + "/" + Items.PageCount; }
        }

        #endregion

        #region Commands

        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new SearchCommand();
                return _searchCommand;
            }
            private set { _searchCommand = value; }
        }

        private RelayCommand _searchResetCommand;

        public RelayCommand SearchResetCommand
        {
            get
            {
                if (_searchResetCommand == null)
                    _searchResetCommand = new SearchResetCommand();
                return _searchResetCommand;
            }
            private set { _searchCommand = value; }
        }

        private RelayCommand _moveToNextPageCommand;

        public RelayCommand MoveToNextPageCommand
        {
            get
            {
                if (_moveToNextPageCommand == null)
                    _moveToNextPageCommand = new MoveToNextPageCommand();
                return _moveToNextPageCommand;
            }
            private set { _moveToNextPageCommand = value; }
        }

        private RelayCommand _moveToPreviousCommand;

        public RelayCommand MoveToPreviousPageCommand
        {
            get
            {
                if (_moveToPreviousCommand == null)
                    _moveToPreviousCommand = new MoveToPreviousPageCommand();
                return _moveToPreviousCommand;
            }
            private set { _moveToPreviousCommand = value; }
        }

        #endregion
    }
}