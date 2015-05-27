using System;
using System.Collections.Generic;
using System.Linq;
using GW2Trader.Command;
using GW2Trader.Model;
using GW2Trader.MVVM;

namespace GW2Trader.ViewModel
{
    public class NewInvestmentViewModel : BaseViewModel
    {
        #region Observable Members

        private string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }

        private GameItemModel _selectedItem;

        public GameItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
                if (SelectedItem != null) { BuyPrice = new Money(SelectedItem.SellPrice); }
                UpdateProfitInformation();
            }
        }

        private int _quantity;

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

        private bool _isSold;

        public bool IsSold
        {
            get { return _isSold; }
            set
            {
                _isSold = value;
                RaisePropertyChanged("IsSold");
            }
        }

        private Money _buyPrice;

        public Money BuyPrice
        {
            get { return _buyPrice; }
            private set
            {
                _buyPrice = value;
                RaisePropertyChanged("BuyPrice");
                UpdateProfitInformation();
            }
        }

        private Money _targetSellPrice;

        public Money TargetSellPrice
        {
            get { return _targetSellPrice; }
            private set
            {
                _targetSellPrice = value;
                RaisePropertyChanged("TargetSellPrice");
                UpdateProfitInformation();
            }
        }

        private Money _sellPrice;

        public Money SellPrice
        {
            get
            {
                return _sellPrice;
            }
            private set
            {
                _sellPrice = value;
                RaisePropertyChanged("SellPrice");
                UpdateProfitInformation();
            }
        }

        #endregion

        public InvestmentModel Investment { get; private set; }

        public NewInvestmentViewModel(IList<GameItemModel> items)
        {
            Items = new PaginatedObservableCollection<GameItemModel>(items);
            Keyword = String.Empty;
            BuyPrice = new Money();
            TargetSellPrice = new Money();
            SellPrice = new Money();
            Quantity = 1;
        }

        public PaginatedObservableCollection<GameItemModel> Items { get; private set; }

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
            Investment = new InvestmentModel
            {
                Count = Quantity,
                GameItem = SelectedItem,
                IsSold = IsSold,
                DesiredSellPrice = TargetSellPrice.Value,
                PurchasePrice = BuyPrice.Value,
                SoldFor = SellPrice.Value
            };
        }
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

        public Money(int val)
        {
            Gold = val / 10000;
            Silver = (val % 10000) / 100;
            Copper = (val % 100);
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
            }
        }

        public int Silver
        {
            get { return _silver; }
            set
            {
                _silver = Math.Min(value, 99);
                RaisePropertyChanged("Silver");
            }
        }

        public int Copper
        {
            get { return _copper; }
            set
            {
                _copper = Math.Min(value, 99);
                RaisePropertyChanged("Copper");
            }
        }

        public int RawValue
        {
            get
            {
                int val = Copper;
                val += Silver * 100;
                val += Gold * 10000;
                return val;
            }
        }
    }
}
