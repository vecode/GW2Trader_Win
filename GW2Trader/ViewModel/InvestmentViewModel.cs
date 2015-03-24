using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using GW2Trader.Command;
using GW2Trader.Data;
using GW2Trader.Model;

namespace GW2Trader.ViewModel
{
    public class InvestmentViewModel : BaseViewModel
    {
        #region Observable Members

        private string _name;
        private string _description;
        private InvestmentWatchlistModel _selectedWatchlist;
        private List<InvestmentWatchlistModel> _selectedWatchlists;
        private ObservableCollection<InvestmentWatchlistModel> _watchlists; 
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
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        public InvestmentWatchlistModel SelectedWatchlist
        {
            get { return _selectedWatchlist; }
            set
            {
                _selectedWatchlist = value;
                RaisePropertyChanged("SelectedWatchlist");
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

        #region Commands

        private RelayCommand _addInvestmentListCommand;

        public RelayCommand AddInvestmentListCommand
        {
            get
            {
                if (_addInvestmentListCommand == null)
                    _addInvestmentListCommand = new AddWatchlistCommand();
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

        #endregion
    }
}