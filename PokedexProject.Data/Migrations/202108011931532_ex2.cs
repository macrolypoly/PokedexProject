namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ex2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Choice", "ChallengeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Choice", "ChallengeId");
        }
    }
}
