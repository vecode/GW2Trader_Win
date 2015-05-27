using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using GW2Trader_Windows.Command;
using GW2Trader_Windows.Data;
using GW2Trader_Windows.Model;
using System;
using System.ComponentModel;
using System.Security.Cryptography;

namespace GW2Trader_Windows.ViewModel
{
    public class InvestmentViewModel : BaseViewModel, IItemViewer
    {
        #region Observable Members

        private string _investmentListName;
        private string _investmentListDescription;
        private InvestmentWatchlistModel _selectedWatchlist;
        private List<InvestmentWatchlistModel> _selectedWatchlists;        
        private ObservableCollection<InvestmentWatchlistModel> _watchlists;

        #endregion

        private readonly IGameDataContextProvider _contextProvider;
        private readonly List<GameItemModel> _items;
        private readonly Dictionary<int, GameItemModel> _itemDictionary;

        public enum SelectionMode
        {
            All,
            Current,
            Past
        };

        public InvestmentViewModel(IGameDataContextProvider contextProvider, List<GameItemModel> items, Dictionary<int, GameItemModel> itemDictionary)
        {
            ViewModelName = "Investments";
            _contextProvider = contextProvider;
            _items = items;
            _itemDictionary = itemDictionary;

            BuildWatchlists();

            if (Watchlists != null && Watchlists.Any())
            {
                SelectedWatchlist = Watchlists[0];
            }
        }

        private void AddItemNotification(GameItemModel item)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyInvestmentStatisticsChanged();
        }

        private void NotifyInvestmentStatisticsChanged()
        {
            RaisePropertyChanged("GoldInvested");
            RaisePropertyChanged("CurrentProfit");
            RaisePropertyChanged("TotalWorth");
        }

        public string InvestmentListName
        {
            get { return _investmentListName; }
            set
            {
                _investmentListName = value;
                RaisePropertyChanged("InvestmentListName");
            }
        }

        public string InvestmentListDescription
        {
            get { return _investmentListDescription; }
            set
            {
                _investmentListDescription = value;
                RaisePropertyChanged("InvestmentListDescription");
            }
        }

        public InvestmentWatchlistModel SelectedWatchlist
        {
            get { return _selectedWatchlist; }
            set
            {
                _selectedWatchlist = value;
                RaisePropertyChanged("SelectedWatchlist");
                NotifyInvestmentStatisticsChanged();
                InvestmentListName = _selectedWatchlist != null ?
                                     _selectedWatchlist.Name : null;
                InvestmentListDescription = _selectedWatchlist != null ?
                    _selectedWatchlist.Description : null;
            }
        }

        public List<InvestmentWatchlistModel> SelectedWatchlists
        {
            get { return _selectedWatchlists; }
            set
            {
                _selectedWatchlists = value;
                RaisePropertyChanged("SelectedWatchlists");
            }
        }

        public ObservableCollection<InvestmentWatchlistModel> Watchlists
        {
            get { return _watchlists; }
            set
            {
                _watchlists = value;
                RaisePropertyChanged("Watchlists");
            }
        }

        public int GoldInvested
        {
            get
            {
                if (SelectedWatchlist != null)
                {
                    return SelectedWatchlist.Items.Sum(inv => inv.Count * inv.PurchasePrice);
                }
                return 0;
            }
        }

        public int CurrentProfit
        {
            get
            {
                if (SelectedWatchlist != null)
                {
                    return SelectedWatchlist.Items.Sum(inv => inv.CurrentTotalProfit);
                }
                return 0;
            }
        }

        public int TotalWorth
        {
            get
            {
                if (SelectedWatchlist != null)
                {
                    return (int)Math.Round(SelectedWatchlist.Items.Sum(inv => inv.GameItem.SellPrice * 0.85f * inv.Count));
                }
                return 0;
            }
        }

        public List<GameItemModel> SharedItems
        {
            get { return _items; }
        }

        public void AddInvestmentList()
        {
            InvestmentWatchlistModel newWatchlist = new InvestmentWatchlistModel
            {
                Name = InvestmentListName,
                Description = InvestmentListDescription
            };
            using (var context = _contextProvider.GetContext())
            {
                context.InvestmentWatchlists.Add(newWatchlist);
                context.Save();
                newWatchlist.Id = context.InvestmentWatchlists.ToList().Last().Id;
            }
            Watchlists.Add(newWatchlist);
        }

        public void UpdateInvestmentList()
        {
            using (var context = _contextProvider.GetContext())
            {
                var listToUpdate = context.InvestmentWatchlists.Single(l => l.Id == SelectedWatchlist.Id);
                listToUpdate.Name = InvestmentListName;
                listToUpdate.Description = InvestmentListDescription;
                context.Save();
            }
            SelectedWatchlist.Name = InvestmentListName;
            SelectedWatchlist.Description = InvestmentListDescription;
        }

        public void DeleteInvestmentList(InvestmentWatchlistModel watchlist)
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToDelete = context.InvestmentWatchlists.Single(wl => wl.Id == watchlist.Id);
                context.InvestmentWatchlists.Remove(watchlistToDelete);
                context.Save();
            }
            Watchlists.Remove(watchlist);
            if (Watchlists.Any())
            {
                SelectedWatchlist = Watchlists.Last();
            }
        }

        public void DeleteInvestment(InvestmentModel investment)
        {
            using (var context = _contextProvider.GetContext())
            {
                InvestmentWatchlistModel contextWatchlist =
                    context.InvestmentWatchlists.Single(wl => wl.Id == SelectedWatchlist.Id);
                InvestmentModel investmentToDelete = context.Investments.Single(inv => inv.Id == investment.Id);
                contextWatchlist.Items.Remove(investmentToDelete);
                context.Investments.Remove(investmentToDelete);
                context.Save();
            }
            SelectedWatchlist.Items.Remove(investment);
        }

        public void AddInvestment(InvestmentModel investment)
        {
            using (var context = _contextProvider.GetContext())
            {
                var contextWatchlists = context.InvestmentWatchlists.Single(wl => wl.Id == SelectedWatchlist.Id);
                context.GameItems.Attach(investment.GameItem);
                contextWatchlists.Items.Add(investment);
                context.Save();
            }
            SelectedWatchlist.Items.Add(investment);
        }

        private void BuildWatchlists()
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlists = context.InvestmentWatchlists.Include(wl => wl.Items.Select(i => i.GameItem)).ToList();
                watchlists.ForEach(wl => wl.Items.ToList()
                    .ForEach(i => i.GameItem = _itemDictionary[i.GameItem.ItemId]));

                foreach (InvestmentWatchlistModel watchlist in watchlists)
                {
                    watchlist.Items.ToList().ForEach(inv => AddItemNotification(inv.GameItem));
                }

                Watchlists = new ObservableCollection<InvestmentWatchlistModel>(watchlists);
            }
        }

        #region Commands

        private RelayCommand _addInvestmentListCommand;

        public RelayCommand AddInvestmentListCommand
        {
            get
            {
                if (_addInvestmentListCommand == null)
                    _addInvestmentListCommand = new AddInvestmentListCommand();
                return _addInvestmentListCommand;
            }
        }

        private RelayCommand _deleteInvestmentListCommand;

        public RelayCommand DeleteInvestmentListCommand
        {
            get
            {
                if (_deleteInvestmentListCommand == null)
                    _deleteInvestmentListCommand = new DeleteInvestmentListCommand();
                return _deleteInvestmentListCommand;
            }
        }

        private RelayCommand _updateInvestmentListCommand;

        public RelayCommand UpdateInvestmentListCommand
        {
            get
            {
                if (_updateInvestmentListCommand == null)
                    _updateInvestmentListCommand = new UpdateInvestmentListCommand();
                return _updateInvestmentListCommand;
            }
        }

        private RelayCommand _newInvestmentDialogCommand;

        public RelayCommand NewInvestmentDialogCommand
        {
            get
            {
                if (_newInvestmentDialogCommand == null)
                    _newInvestmentDialogCommand = new OpenNewInvestmentDialogCommand();
                return _newInvestmentDialogCommand;
            }
        }

        #endregion

        public IList<GameItemModel> ShownItems
        {
            get
            {
                if (SelectedWatchlist != null) return SelectedWatchlist.Items.Select(inv => inv.GameItem).ToList();
                return null;
            }
        }
    }
}