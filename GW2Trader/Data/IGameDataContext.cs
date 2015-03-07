using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public interface IGameDataContext : IDisposable
    {
        DbSet<GameItemModel> GameItems { get; }
        DbSet<InvestmentWatchlistModel> InvestmentWatchlists { get; }
        DbSet<ItemIdWatchlistModel> ItemIdWatchlists { get; }
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry Entry<T>(T entity) where T : class;
        void Save();        
    }
}
