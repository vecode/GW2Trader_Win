namespace GW2Trader_Windows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvestmentTableadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvestmentModels", "GameItem_ItemId", "dbo.GameItemModels");
            DropIndex("dbo.InvestmentModels", new[] { "GameItem_ItemId" });
            AlterColumn("dbo.InvestmentModels", "GameItem_ItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvestmentModels", "GameItem_ItemId");
            AddForeignKey("dbo.InvestmentModels", "GameItem_ItemId", "dbo.GameItemModels", "ItemId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvestmentModels", "GameItem_ItemId", "dbo.GameItemModels");
            DropIndex("dbo.InvestmentModels", new[] { "GameItem_ItemId" });
            AlterColumn("dbo.InvestmentModels", "GameItem_ItemId", c => c.Int());
            CreateIndex("dbo.InvestmentModels", "GameItem_ItemId");
            AddForeignKey("dbo.InvestmentModels", "GameItem_ItemId", "dbo.GameItemModels", "ItemId");
        }
    }
}
