namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ex2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Answers", newName: "Answer");
            RenameTable(name: "dbo.Choices", newName: "Choice");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Choice", newName: "Choices");
            RenameTable(name: "dbo.Answer", newName: "Answers");
        }
    }
}
