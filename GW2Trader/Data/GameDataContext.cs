using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace GW2Trader.Data
{
    public class GameDataContext : DbContext, IGameDataContext
    {
        public DbSet<GameItemModel> GameItems { get; set; }
        public DbSet<InvestmentWatchlistModel> InvestmentWatchlists { get; set; }
        public DbSet<ItemIdWatchlistModel> ItemIdWatchlists { get; set; }

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

        public void Save()
        {
            this.SaveChanges();
        }
    }
}
