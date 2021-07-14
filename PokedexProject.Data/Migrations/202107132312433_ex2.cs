namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ex2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "Route_RouteId", "dbo.Route");
            DropForeignKey("dbo.Pokemon", "Route_RouteId", "dbo.Route");
            DropIndex("dbo.Item", new[] { "Route_RouteId" });
            DropIndex("dbo.Pokemon", new[] { "Route_RouteId" });
            CreateTable(
                "dbo.RouteItem",
                c => new
                    {
                        Route_RouteId = c.Int(nullable: false),
                        Item_ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Route_RouteId, t.Item_ItemId })
                .ForeignKey("dbo.Route", t => t.Route_RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.Item_ItemId, cascadeDelete: true)
                .Index(t => t.Route_RouteId)
                .Index(t => t.Item_ItemId);
            
            CreateTable(
                "dbo.PokemonRoute",
                c => new
                    {
                        Pokemon_PokemonId = c.Int(nullable: false),
                        Route_RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pokemon_PokemonId, t.Route_RouteId })
                .ForeignKey("dbo.Pokemon", t => t.Pokemon_PokemonId, cascadeDelete: true)
                .ForeignKey("dbo.Route", t => t.Route_RouteId, cascadeDelete: true)
                .Index(t => t.Pokemon_PokemonId)
                .Index(t => t.Route_RouteId);
            
            DropColumn("dbo.Item", "Route_RouteId");
            DropColumn("dbo.Pokemon", "Route_RouteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pokemon", "Route_RouteId", c => c.Int());
            AddColumn("dbo.Item", "Route_RouteId", c => c.Int());
            DropForeignKey("dbo.PokemonRoute", "Route_RouteId", "dbo.Route");
            DropForeignKey("dbo.PokemonRoute", "Pokemon_PokemonId", "dbo.Pokemon");
            DropForeignKey("dbo.RouteItem", "Item_ItemId", "dbo.Item");
            DropForeignKey("dbo.RouteItem", "Route_RouteId", "dbo.Route");
            DropIndex("dbo.PokemonRoute", new[] { "Route_RouteId" });
            DropIndex("dbo.PokemonRoute", new[] { "Pokemon_PokemonId" });
            DropIndex("dbo.RouteItem", new[] { "Item_ItemId" });
            DropIndex("dbo.RouteItem", new[] { "Route_RouteId" });
            DropTable("dbo.PokemonRoute");
            DropTable("dbo.RouteItem");
            CreateIndex("dbo.Pokemon", "Route_RouteId");
            CreateIndex("dbo.Item", "Route_RouteId");
            AddForeignKey("dbo.Pokemon", "Route_RouteId", "dbo.Route", "RouteId");
            AddForeignKey("dbo.Item", "Route_RouteId", "dbo.Route", "RouteId");
        }
    }
}
