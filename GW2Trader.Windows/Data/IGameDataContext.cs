using System;
using System.Collections.Generic;
using System.Data.Entity;
using GW2Trader.Desktop.Model;

namespace GW2Trader.Desktop.Data
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
