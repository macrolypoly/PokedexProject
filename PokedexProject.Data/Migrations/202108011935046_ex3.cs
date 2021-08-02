namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ex3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Choice", "ChallengeId");
            AddForeignKey("dbo.Choice", "ChallengeId", "dbo.Challenge", "ChallengeId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Choice", "ChallengeId", "dbo.Challenge");
            DropIndex("dbo.Choice", new[] { "ChallengeId" });
        }
    }
}
