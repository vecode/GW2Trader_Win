using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using GW2TPApiWrapper.Enums;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace GW2Trader.Data
{
    public partial class GameDataContext : DbContext, IGameDataContext
    {
        public DbSet<GameItemModel> GameItems { get; set; }
        public DbSet<InvestmentWatchlistModel> InvestmentWatchlists { get; set; }
        public DbSet<ItemIdWatchlistModel> ItemIdWatchlists { get; set; }

        public GameDataContext()
            : base("ItemDb.DbConnection")
        {
            Database.SetInitializer<GameDataContext>(new DbInitializer());            
        }

        class DbInitializer : DropCreateDatabaseAlways<GameDataContext>
        {
            protected override void Seed(GameDataContext context)
            {
                base.Seed(context);
            }
        }

        public void Save()
        {
            this.SaveChanges();
        }
    }

}
