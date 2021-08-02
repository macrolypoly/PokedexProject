namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        AnswerText = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId);
            
            CreateTable(
                "dbo.Challenge",
                c => new
                    {
                        ChallengeId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ChallengeName = c.String(),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChallengeId)
                .ForeignKey("dbo.PokeRoute", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        QuestionText = c.String(),
                        ChallengeId = c.Int(nullable: false),
                        AnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Answer", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.Challenge", t => t.ChallengeId, cascadeDelete: true)
                .Index(t => t.ChallengeId)
                .Index(t => t.AnswerId);
            
            CreateTable(
                "dbo.Choice",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ChoiceText = c.String(),
                        QuestionId = c.Int(nullable: false),
                        MyAnswer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ChoiceId)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.PokeRoute",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        RouteName = c.String(),
                    })
                .PrimaryKey(t => t.RouteId);
            
            CreateTable(
                "dbo.PokeItem",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ItemName = c.String(),
                        Description = c.String(),
                        Trainer_TrainerId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Trainer", t => t.Trainer_TrainerId)
                .Index(t => t.Trainer_TrainerId);
            
            CreateTable(
                "dbo.Pokemon",
                c => new
                    {
                        PokemonId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        PokemonName = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Type2 = c.Int(nullable: false),
                        Trainer_TrainerId = c.Int(),
                    })
                .PrimaryKey(t => t.PokemonId)
                .ForeignKey("dbo.Trainer", t => t.Trainer_TrainerId)
                .Index(t => t.Trainer_TrainerId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TrainerItems",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        ItemName = c.String(),
                        TrainerId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.PokeItem", t => t.ItemId)
                .ForeignKey("dbo.Trainer", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.Trainer",
                c => new
                    {
                        TrainerId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        TrainerName = c.String(),
                        ProfileCreated = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.TrainerId);
            
            CreateTable(
                "dbo.TrainerPokemon",
                c => new
                    {
                        PokemonId = c.Int(nullable: false),
                        PokemonName = c.String(),
                        TrainerId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PokemonId)
                .ForeignKey("dbo.Pokemon", t => t.PokemonId)
                .ForeignKey("dbo.Trainer", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.PokemonId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.PokeItemPokeRoute",
                c => new
                    {
                        PokeItem_ItemId = c.Int(nullable: false),
                        PokeRoute_RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PokeItem_ItemId, t.PokeRoute_RouteId })
                .ForeignKey("dbo.PokeItem", t => t.PokeItem_ItemId, cascadeDelete: true)
                .ForeignKey("dbo.PokeRoute", t => t.PokeRoute_RouteId, cascadeDelete: true)
                .Index(t => t.PokeItem_ItemId)
                .Index(t => t.PokeRoute_RouteId);
            
            CreateTable(
                "dbo.PokemonPokeRoute",
                c => new
                    {
                        Pokemon_PokemonId = c.Int(nullable: false),
                        PokeRoute_RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pokemon_PokemonId, t.PokeRoute_RouteId })
                .ForeignKey("dbo.Pokemon", t => t.Pokemon_PokemonId, cascadeDelete: true)
                .ForeignKey("dbo.PokeRoute", t => t.PokeRoute_RouteId, cascadeDelete: true)
                .Index(t => t.Pokemon_PokemonId)
                .Index(t => t.PokeRoute_RouteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.TrainerPokemon", "TrainerId", "dbo.Trainer");
            DropForeignKey("dbo.TrainerPokemon", "PokemonId", "dbo.Pokemon");
            DropForeignKey("dbo.TrainerItems", "TrainerId", "dbo.Trainer");
            DropForeignKey("dbo.Pokemon", "Trainer_TrainerId", "dbo.Trainer");
            DropForeignKey("dbo.PokeItem", "Trainer_TrainerId", "dbo.Trainer");
            DropForeignKey("dbo.TrainerItems", "ItemId", "dbo.PokeItem");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Challenge", "RouteId", "dbo.PokeRoute");
            DropForeignKey("dbo.PokemonPokeRoute", "PokeRoute_RouteId", "dbo.PokeRoute");
            DropForeignKey("dbo.PokemonPokeRoute", "Pokemon_PokemonId", "dbo.Pokemon");
            DropForeignKey("dbo.PokeItemPokeRoute", "PokeRoute_RouteId", "dbo.PokeRoute");
            DropForeignKey("dbo.PokeItemPokeRoute", "PokeItem_ItemId", "dbo.PokeItem");
            DropForeignKey("dbo.Choice", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Question", "ChallengeId", "dbo.Challenge");
            DropForeignKey("dbo.Question", "AnswerId", "dbo.Answer");
            DropIndex("dbo.PokemonPokeRoute", new[] { "PokeRoute_RouteId" });
            DropIndex("dbo.PokemonPokeRoute", new[] { "Pokemon_PokemonId" });
            DropIndex("dbo.PokeItemPokeRoute", new[] { "PokeRoute_RouteId" });
            DropIndex("dbo.PokeItemPokeRoute", new[] { "PokeItem_ItemId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TrainerPokemon", new[] { "TrainerId" });
            DropIndex("dbo.TrainerPokemon", new[] { "PokemonId" });
            DropIndex("dbo.TrainerItems", new[] { "TrainerId" });
            DropIndex("dbo.TrainerItems", new[] { "ItemId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Pokemon", new[] { "Trainer_TrainerId" });
            DropIndex("dbo.PokeItem", new[] { "Trainer_TrainerId" });
            DropIndex("dbo.Choice", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "AnswerId" });
            DropIndex("dbo.Question", new[] { "ChallengeId" });
            DropIndex("dbo.Challenge", new[] { "RouteId" });
            DropTable("dbo.PokemonPokeRoute");
            DropTable("dbo.PokeItemPokeRoute");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.TrainerPokemon");
            DropTable("dbo.Trainer");
            DropTable("dbo.TrainerItems");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Pokemon");
            DropTable("dbo.PokeItem");
            DropTable("dbo.PokeRoute");
            DropTable("dbo.Choice");
            DropTable("dbo.Question");
            DropTable("dbo.Challenge");
            DropTable("dbo.Answer");
        }
    }
}
