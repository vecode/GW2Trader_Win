using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Command;
using GW2Trader.Model;
using GW2Trader.MVVM;
using GW2Trader.Util;

namespace GW2Trader.ViewModel
{
    public class NewInvestmentViewModel : BaseViewModel
    {
        private List<GameItemModel> _items;

        #region Observable Members

        private string _keyword;
        private GameItemModel _selectedItem;
        private int _quantity;
        private bool _isSold;

        public Money BuyPrice { get; private set; }
        public Money TargetSellPrice { get; private set; }
        public Money SellPrice { get; private set; }

        #endregion

        public NewInvestmentViewModel(IList<GameItemModel> items)
        {
            _items = items.ToList();
            Items = new PaginatedObservableCollection<GameItemModel>(_items);
            Keyword = String.Empty;
            BuyPrice = new Money();
            TargetSellPrice = new Money();
            SellPrice = new Money();
            Quantity = 1;
        }

        public PaginatedObservableCollection<GameItemModel> Items { get; private set; }

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }

        public GameItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged("Quantity");
            }
        }

        public bool IsSold
        {
            get { return _isSold;}
            set 
            { 
                _isSold = value;
                RaisePropertyChanged("IsSold"); 
            }
        }

        #region Commands

        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new KeywordSearchCommand();
                return _searchCommand;
            }
        }

        private RelayCommand _moveToPreviousPageCommand;

        public RelayCommand MoveToPreviousPageCommand
        {
            get
            {
                if (_moveToPreviousPageCommand == null)
                    _moveToPreviousPageCommand = new MoveToPreviousPageCommand();
                return _moveToPreviousPageCommand;
            }
        }

        private MoveToNextPageCommand _moveToNextPageCommand;

        public RelayCommand MoveToNextPageCommand
        {
            get
            {
                if (_moveToNextPageCommand == null)
                    _moveToNextPageCommand = new MoveToNextPageCommand();
                return _moveToNextPageCommand;
            }
        }


        #endregion
    }

    public class Money : ObservableObject
    {
        private int _gold;
        private int _silver;
        private int _copper;

        public Money()
        {
            Gold = 0;
            Silver = 0;
            Copper = 0;
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                RaisePropertyChanged("Gold");
            }
        }

        public int Silver
        {
            get { return _silver; }
            set
            {
                _silver = value;
                RaisePropertyChanged("Silver");
            }
        }

        public int Copper
        {
            get { return _copper; }
            set
            {
                _copper = value;
                RaisePropertyChanged("Copper");
            }
        }
    }
}
