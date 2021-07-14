namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editmodel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Route", newName: "PokeRoute");
            RenameTable(name: "dbo.RouteItem", newName: "PokeRouteItem");
            RenameTable(name: "dbo.PokemonRoute", newName: "PokemonPokeRoute");
            RenameColumn(table: "dbo.PokeRouteItem", name: "Route_RouteId", newName: "PokeRoute_RouteId");
            RenameColumn(table: "dbo.PokemonPokeRoute", name: "Route_RouteId", newName: "PokeRoute_RouteId");
            RenameIndex(table: "dbo.PokeRouteItem", name: "IX_Route_RouteId", newName: "IX_PokeRoute_RouteId");
            RenameIndex(table: "dbo.PokemonPokeRoute", name: "IX_Route_RouteId", newName: "IX_PokeRoute_RouteId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PokemonPokeRoute", name: "IX_PokeRoute_RouteId", newName: "IX_Route_RouteId");
            RenameIndex(table: "dbo.PokeRouteItem", name: "IX_PokeRoute_RouteId", newName: "IX_Route_RouteId");
            RenameColumn(table: "dbo.PokemonPokeRoute", name: "PokeRoute_RouteId", newName: "Route_RouteId");
            RenameColumn(table: "dbo.PokeRouteItem", name: "PokeRoute_RouteId", newName: "Route_RouteId");
            RenameTable(name: "dbo.PokemonPokeRoute", newName: "PokemonRoute");
            RenameTable(name: "dbo.PokeRouteItem", newName: "RouteItem");
            RenameTable(name: "dbo.PokeRoute", newName: "Route");
        }
    }
}
