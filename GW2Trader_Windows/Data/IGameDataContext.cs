using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Model;

namespace GW2Trader_Windows.Data
{
    public interface IGameDataContext : IDisposable
    {
        IDbSet<GameItemModel> GameItems { get; set; }
        IDbSet<InvestmentModel> Investments { get; set; }
        IDbSet<InvestmentWatchlistModel> InvestmentWatchlists { get; set; }
        IDbSet<ItemWatchlistModel> ItemWatchlists { get; set; }

        void Save();
        void BulkInsert(IList<GameItemModel> items);
    }
}
