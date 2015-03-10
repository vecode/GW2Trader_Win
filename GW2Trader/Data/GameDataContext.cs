using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using GW2TPApiWrapper.Enum;
using GW2Trader.Model;
using EntityFramework.BulkInsert.Extensions;
using ErikEJ.SqlCe;
using System.Data.Entity.Core.Objects;
using System.Text.RegularExpressions;

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

        public void BulkInsert(IList<GameItemModel> items)
        {
            using (SqlCeBulkCopy bcp = new SqlCeBulkCopy(Database.Connection.ConnectionString))
            {
                bcp.DestinationTableName = "GameItemModels";
                bcp.WriteToServer(items);
            }
        }
    }
}
