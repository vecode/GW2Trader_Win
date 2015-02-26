using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Data;
using GW2Trader.Model;
using System.Windows.Data;

namespace GW2Trader.ViewModel
{
    public class ItemOverviewViewModel : BaseViewModel
    {
        private IGameDataRepository _dataRepository;
        private ITradingPostApiWrapper _tradingPostApiWrapper;
        private IApiDataUpdater _apiDataUpdater;
        private DbBuilder _dbBuilder;

        
        public CollectionViewSource Items { get; set; }
        private ObservableCollection<GameItemModel> _filteredItems { get; set; }
        public ObservableCollection<GameItemModel> SelectedItems { get; private set; }



        public ItemOverviewViewModel() { }

        public ItemOverviewViewModel(
            IGameDataRepository dataRepository, 
            ITradingPostApiWrapper tradingPostApiWrapper,
            IApiDataUpdater apiDataUpdater,
            DbBuilder dbBuilder)
        {
            _dataRepository = dataRepository;
            _tradingPostApiWrapper = tradingPostApiWrapper;
            _apiDataUpdater = apiDataUpdater;
            _dbBuilder = dbBuilder;


            Items = new CollectionViewSource();            
            
            if (_dataRepository.GetAllGameItems().Count() == 0)
                _dbBuilder.BuildDatabase();
            
            _filteredItems = new ObservableCollection<GameItemModel>(_dataRepository.GetAllGameItems());
            Items.Source = _filteredItems;           
            Items.View.Refresh();
            Console.WriteLine("FilteredItems set, count: " + _filteredItems.Count);
            
        }
    }
}
