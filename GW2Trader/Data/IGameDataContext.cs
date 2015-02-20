using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public interface IGameDataContext
    {
        DbSet<GameItemModel> GameItems { get; }
        DbSet<InvestmentWatchlistModel> InvestmentWatchlists { get; }
        DbSet<ItemIdWatchlistModel> ItemIdWatchlists { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Save();
    }
}
