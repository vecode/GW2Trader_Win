using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using GW2TPApiWrapper.Enum;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using EntityFramework.BulkInsert.Extensions;

namespace GW2Trader.Data
{
    public partial class GameDataContext : DbContext
    {
        public DbSet<GameItemModel> GameItems { get; set; }
        public DbSet<InvestmentWatchlistModel> InvestmentWatchlists { get; set; }
        public DbSet<ItemIdWatchlistModel> ItemIdWatchlists { get; set; }

        public GameDataContext()
            : base("ItemDb.DbConnection")
        {
            Database.SetInitializer<GameDataContext>(new DbInitializer());
        }

        class DbInitializer : DropCreateDatabaseIfModelChanges<GameDataContext>
        {
            protected override void Seed(GameDataContext context)
            {
                base.Seed(context);
            }
        }

        public void Save()
        {
            int b = this.SaveChanges();
        }
    }
}
