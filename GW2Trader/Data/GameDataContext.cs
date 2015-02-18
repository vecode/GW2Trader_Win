using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public class GameDataContext : DbContext, IGameDataContext
    {
        public IDbSet<GameItemModel> GameItems { get; set; }
        public IDbSet<Watchlist<InvestmentModel>> Investments { get; set; }
        public IDbSet<Watchlist<int>> WatchedItemIds { get; set; }

        static GameDataContext()
        {
            Database.SetInitializer<GameDataContext>(new DbInitializer());
        }

        class DbInitializer : DropCreateDatabaseIfModelChanges<GameDataContext>
        {
            protected override void Seed(GameDataContext context)
            {
                // TODO add seed
                base.Seed(context);
            }
        }
    }
}
