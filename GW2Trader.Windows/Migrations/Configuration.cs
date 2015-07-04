using System.Data.Entity.Migrations;
using GW2Trader.Desktop.Data;

namespace GW2Trader.Desktop.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GameDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GW2Trader.Data.GameDataContext";
        }

        protected override void Seed(GameDataContext context)
        {
        }
    }
}
