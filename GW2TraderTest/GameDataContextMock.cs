using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GW2Trader.Model;
using GW2Trader.Data;
using GW2TPApiWrapper.Wrapper;

namespace GW2TraderTest
{
    public class GameDataContextMock : IGameDataContext
    {
        private GameDataContext _context = new GameDataContext();
        private TestGameDataFactory _dataFactory = new TestGameDataFactory();

        public GameDataContextMock()
        {
            _context.Database.Delete();

            AddToContext(_dataFactory.GetTestGameItems());
            AddToContext(_dataFactory.GetTestItemIdWatchlists());
            AddToContext(_dataFactory.GetTestInvestmentWatchlists());
            Save();
        }

        public DbSet<GameItemModel> GameItems
        {
            get { return _context.GameItems; }
        }

        public DbSet<InvestmentWatchlistModel> InvestmentWatchlists
        {
            get { return _context.InvestmentWatchlists; }
        }

        public DbSet<ItemIdWatchlistModel> ItemIdWatchlists
        {
            get { return _context.ItemIdWatchlists; }
        }


        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void Save()
        {
            _context.Save();
        }

        private void AddToContext<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
        }
    }
}
