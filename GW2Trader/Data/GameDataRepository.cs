using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using System.Collections.ObjectModel;

namespace GW2Trader.Data
{
    public class GameDataRepository : IGameDataRepository
    {
        private IGameDataContext _context;
        private Dictionary<int, GameItemModel> _gameItemDictionary;
        private ObservableCollection<Watchlist<InvestmentModel>> _investments;

        public GameDataRepository(IGameDataContext context)
        {
            _context = context;

            _gameItemDictionary = new Dictionary<int, GameItemModel>();
            if (_context.GameItems != null)
            {
                _gameItemDictionary = _context.GameItems.ToDictionary(item => item.Id, item => item);
            }

            if (_context.Investments != null)
            {
                _investments = new ObservableCollection<Watchlist<InvestmentModel>>(_context.Investments);
            }
            else
            {
                _investments = new ObservableCollection<Watchlist<InvestmentModel>>();
            }
        }

        public ObservableCollection<Watchlist<InvestmentModel>> Investments
        {
            get
            {
                return _investments;
            }
        }

        public IEnumerable<Watchlist<GameItemModel>> Items
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public GameItemModel ItemById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameItemModel> ItemsById(int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
