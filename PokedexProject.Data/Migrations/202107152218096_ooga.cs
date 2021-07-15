namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ooga : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Item", newName: "PokeItem");
            RenameTable(name: "dbo.PokeRouteItem", newName: "PokeRoutePokeItem");
            RenameColumn(table: "dbo.PokeRoutePokeItem", name: "Item_ItemId", newName: "PokeItem_ItemId");
            RenameIndex(table: "dbo.PokeRoutePokeItem", name: "IX_Item_ItemId", newName: "IX_PokeItem_ItemId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PokeRoutePokeItem", name: "IX_PokeItem_ItemId", newName: "IX_Item_ItemId");
            RenameColumn(table: "dbo.PokeRoutePokeItem", name: "PokeItem_ItemId", newName: "Item_ItemId");
            RenameTable(name: "dbo.PokeRoutePokeItem", newName: "PokeRouteItem");
            RenameTable(name: "dbo.PokeItem", newName: "Item");
        }
    }
}
