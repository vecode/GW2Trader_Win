namespace GW2Trader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameItemModels",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 4000),
                        Rarity = c.String(nullable: false, maxLength: 4000),
                        Type = c.String(nullable: false, maxLength: 4000),
                        SubType = c.String(maxLength: 4000),
                        RestrictionLevel = c.Int(nullable: false),
                        IconUrl = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.InvestmentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsSold = c.Boolean(nullable: false),
                        PurchasePrice = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DesiredSellPrice = c.Int(),
                        SoldFor = c.Int(),
                        GameItem_ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameItemModels", t => t.GameItem_ItemId)
                .Index(t => t.GameItem_ItemId);
            
            CreateTable(
                "dbo.InvestmentWatchlistModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemWatchlistModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvestmentWatchlistModelInvestmentModels",
                c => new
                    {
                        InvestmentWatchlistModel_Id = c.Int(nullable: false),
                        InvestmentModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InvestmentWatchlistModel_Id, t.InvestmentModel_Id })
                .ForeignKey("dbo.InvestmentWatchlistModels", t => t.InvestmentWatchlistModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.InvestmentModels", t => t.InvestmentModel_Id, cascadeDelete: true)
                .Index(t => t.InvestmentWatchlistModel_Id)
                .Index(t => t.InvestmentModel_Id);
            
            CreateTable(
                "dbo.ItemWatchlistModelGameItemModels",
                c => new
                    {
                        ItemWatchlistModel_Id = c.Int(nullable: false),
                        GameItemModel_ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemWatchlistModel_Id, t.GameItemModel_ItemId })
                .ForeignKey("dbo.ItemWatchlistModels", t => t.ItemWatchlistModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.GameItemModels", t => t.GameItemModel_ItemId, cascadeDelete: true)
                .Index(t => t.ItemWatchlistModel_Id)
                .Index(t => t.GameItemModel_ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemWatchlistModelGameItemModels", "GameItemModel_ItemId", "dbo.GameItemModels");
            DropForeignKey("dbo.ItemWatchlistModelGameItemModels", "ItemWatchlistModel_Id", "dbo.ItemWatchlistModels");
            DropForeignKey("dbo.InvestmentWatchlistModelInvestmentModels", "InvestmentModel_Id", "dbo.InvestmentModels");
            DropForeignKey("dbo.InvestmentWatchlistModelInvestmentModels", "InvestmentWatchlistModel_Id", "dbo.InvestmentWatchlistModels");
            DropForeignKey("dbo.InvestmentModels", "GameItem_ItemId", "dbo.GameItemModels");
            DropIndex("dbo.ItemWatchlistModelGameItemModels", new[] { "GameItemModel_ItemId" });
            DropIndex("dbo.ItemWatchlistModelGameItemModels", new[] { "ItemWatchlistModel_Id" });
            DropIndex("dbo.InvestmentWatchlistModelInvestmentModels", new[] { "InvestmentModel_Id" });
            DropIndex("dbo.InvestmentWatchlistModelInvestmentModels", new[] { "InvestmentWatchlistModel_Id" });
            DropIndex("dbo.InvestmentModels", new[] { "GameItem_ItemId" });
            DropTable("dbo.ItemWatchlistModelGameItemModels");
            DropTable("dbo.InvestmentWatchlistModelInvestmentModels");
            DropTable("dbo.ItemWatchlistModels");
            DropTable("dbo.InvestmentWatchlistModels");
            DropTable("dbo.InvestmentModels");
            DropTable("dbo.GameItemModels");
        }
    }
}
