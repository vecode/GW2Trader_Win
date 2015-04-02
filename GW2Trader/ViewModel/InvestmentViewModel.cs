using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using GW2Trader.Command;
using GW2Trader.Data;
using GW2Trader.Model;
using GW2Trader.Decorator;

namespace GW2Trader.ViewModel
{
    public class InvestmentViewModel : BaseViewModel
    {
        #region Observable Members

        private string _investmentListName;
        private string _investmentListDescription;
        private InvestmentWatchlistDecorator _selectedWatchlist;
        private List<InvestmentWatchlistDecorator> _selectedWatchlists;
        private ObservableCollection<InvestmentWatchlistDecorator> _watchlists;
        #endregion

        private readonly IGameDataContextProvider _contextProvider;
        private List<GameItemModel> _items;

        public enum SelectionMode
        {
            All,
            Current,
            Past
        };

        public InvestmentViewModel(IGameDataContextProvider contextProvider, List<GameItemModel> items)
        {
            ViewModelName = "Investments";
            _contextProvider = contextProvider;
            _items = items;

            BuildWatchlists();

            if (Watchlists != null && Watchlists.Any())
            {
                SelectedWatchlist = Watchlists[0];
            }
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

        public InvestmentWatchlistDecorator SelectedWatchlist
        {
            get { return _selectedWatchlist; }
            set
            {
                _selectedWatchlist = value;
                RaisePropertyChanged("SelectedWatchlist");
                InvestmentListName = _selectedWatchlist != null ?
                                     _selectedWatchlist.Name : null;
                InvestmentListDescription = _selectedWatchlist != null ?
                    _selectedWatchlist.Description : null;
            }
        }

        public List<InvestmentWatchlistDecorator> SelectedWatchlists
        {
            get { return _selectedWatchlists; }
            set
            {
                _selectedWatchlists = value;
                RaisePropertyChanged("SelectedWatchlists");
            }
        }

        public ObservableCollection<InvestmentWatchlistDecorator> Watchlists
        {
            get { return _watchlists; }
            set
            {
                _watchlists = value;
                RaisePropertyChanged("Watchlists");
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
            Watchlists.Add(InvestmentWatchlistDecorator.Decorate(newWatchlist));
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

        public void DeleteInvestmentList(InvestmentWatchlistDecorator watchlist)
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlistToDelete = context.InvestmentWatchlists.Single(wl => wl.Id == watchlist.Id);
                context.InvestmentWatchlists.Remove(watchlistToDelete);
                context.Save();
            }
            _watchlists.Remove(watchlist);
            if (Watchlists.Any())
            {
                SelectedWatchlist = Watchlists.Last();
            }
        }

        public void DeleteInvestment(InvestmentDecorator investment)
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
            SelectedWatchlist.Items.Add(InvestmentDecorator.Decorate(investment));
        }

        private void BuildWatchlists()
        {
            using (var context = _contextProvider.GetContext())
            {
                var watchlists = context.InvestmentWatchlists.Include(wl => wl.Items.Select(i => i.GameItem)).ToList();
                watchlists.ForEach(wl => wl.Items.ToList()
                    .ForEach(i => i.GameItem = _items.Single(item => item.ItemId == i.GameItem.ItemId)));
                Watchlists = new ObservableCollection<InvestmentWatchlistDecorator>(InvestmentWatchlistDecorator.Decorate(watchlists));
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
    }
}