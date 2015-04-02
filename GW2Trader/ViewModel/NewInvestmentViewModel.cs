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
        private delegate void NotifyMethod();

        #region Observable Members

        private string _keyword;
        private GameItemModel _selectedItem;
        private int _quantity;
        private bool _isSold;

        public Money BuyPrice { get; private set; }
        public Money TargetSellPrice { get; private set; }
        public Money SellPrice { get; private set; }

        #endregion

        public InvestmentModel Investment { get; private set; }

        public NewInvestmentViewModel(IList<GameItemModel> items)
        {
            _items = items.ToList();
            Items = new PaginatedObservableCollection<GameItemModel>(_items);
            Keyword = String.Empty;
            BuyPrice = new Money(UpdateProfitInformation);
            TargetSellPrice = new Money(UpdateProfitInformation);
            SellPrice = new Money(UpdateProfitInformation);
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
                UpdateProfitInformation();
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged("Quantity");
                UpdateProfitInformation();
            }
        }

        public bool IsSold
        {
            get { return _isSold; }
            set
            {
                _isSold = value;
                RaisePropertyChanged("IsSold");
            }
        }

        public int GoldInvested
        {
            get { return Quantity * BuyPrice.Value; }
        }

        public int CurrentProfitPerUnit
        {
            get
            {
                return SelectedItem != null ?
                    (int)Math.Round(SelectedItem.SellPrice * 0.85f - BuyPrice.Value) : 0;
            }
        }

        public int ExpectedProfitPerUnit
        {
            get
            {
                return (int)Math.Round(TargetSellPrice.Value * 0.85f - BuyPrice.Value);
            }
        }

        public int CurrentTotalProfit
        {
            get { return CurrentProfitPerUnit * Quantity; }
        }

        public int ExpectedTotalProfit
        {
            get { return ExpectedProfitPerUnit * Quantity; }
        }

        #region Commands

        private RelayCommand _addInvestmentCommand;

        public RelayCommand AddInvestmentCommand
        {
            get
            {
                if (_addInvestmentCommand == null)
                    _addInvestmentCommand = new AddInvestmentCommand();
                return _addInvestmentCommand;
            }
        }

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

        private void UpdateProfitInformation()
        {
            RaisePropertyChanged("GoldInvested");
            RaisePropertyChanged("CurrentProfitPerUnit");
            RaisePropertyChanged("ExpectedProfitPerUnit");
            RaisePropertyChanged("CurrentTotalProfit");
            RaisePropertyChanged("ExpectedTotalProfit");
        }

        public void FinalizeResult()
        {
            Investment = new InvestmentModel();
            Investment.Count = Quantity;
            Investment.GameItem = SelectedItem;
            Investment.IsSold = IsSold;
            Investment.DesiredSellPrice = TargetSellPrice.Value;
            Investment.PurchasePrice = BuyPrice.Value;
            Investment.SoldFor = SellPrice.Value;
        }
    }

    public class Money : ObservableObject
    {
        private int _gold;
        private int _silver;
        private int _copper;
        Action _notifyMethod;

        public Money(Action notifyMethod)
        {
            _notifyMethod = notifyMethod;
            Gold = 0;
            Silver = 0;
            Copper = 0;
        }

        public int Value
        {
            get { return Copper + (100 * Silver) + (10000 * Gold); }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                RaisePropertyChanged("Gold");
                _notifyMethod();
            }
        }

        public int Silver
        {
            get { return _silver; }
            set
            {
                _silver = Math.Min(value, 99);
                RaisePropertyChanged("Silver");
                _notifyMethod();
            }
        }

        public int Copper
        {
            get { return _copper; }
            set
            {
                _copper = Math.Min(value, 99);
                RaisePropertyChanged("Copper");
                _notifyMethod();
            }
        }
    }
}
