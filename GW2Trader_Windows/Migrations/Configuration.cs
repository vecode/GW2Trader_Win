namespace GW2Trader_Windows.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GW2Trader_Windows.Data.GameDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GW2Trader.Data.GameDataContext";
        }

        protected override void Seed(GW2Trader_Windows.Data.GameDataContext context)
        {
        }
    }
}
