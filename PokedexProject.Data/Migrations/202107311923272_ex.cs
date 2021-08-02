namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Choice", "MyAnswer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Choice", "MyAnswer", c => c.Boolean(nullable: false));
        }
    }
}
