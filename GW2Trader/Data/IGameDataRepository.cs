using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public interface IGameDataRepository
    {
        ObservableCollection<Watchlist<InvestmentModel>> Investments { get; }
        IEnumerable<Watchlist<GameItemModel>> Items { get; }
        GameItemModel ItemById(int id);
        IEnumerable<GameItemModel> ItemsById(int[] ids);
    }
}
