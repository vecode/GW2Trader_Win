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

        public override int SaveChanges()
        {

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);
                List<string> errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

    }

}
