namespace PokedexProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ex : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ItemName = c.String(),
                        Description = c.String(),
                        Route_RouteId = c.Int(),
                        Trainer_TrainerId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Route", t => t.Route_RouteId)
                .ForeignKey("dbo.Trainer", t => t.Trainer_TrainerId)
                .Index(t => t.Route_RouteId)
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
                        Route_RouteId = c.Int(),
                        Trainer_TrainerId = c.Int(),
                    })
                .PrimaryKey(t => t.PokemonId)
                .ForeignKey("dbo.Route", t => t.Route_RouteId)
                .ForeignKey("dbo.Trainer", t => t.Trainer_TrainerId)
                .Index(t => t.Route_RouteId)
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
                "dbo.Route",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        RouteName = c.String(),
                    })
                .PrimaryKey(t => t.RouteId);
            
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
                .ForeignKey("dbo.Item", t => t.ItemId)
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
            DropForeignKey("dbo.Item", "Trainer_TrainerId", "dbo.Trainer");
            DropForeignKey("dbo.TrainerItems", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Pokemon", "Route_RouteId", "dbo.Route");
            DropForeignKey("dbo.Item", "Route_RouteId", "dbo.Route");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TrainerPokemon", new[] { "TrainerId" });
            DropIndex("dbo.TrainerPokemon", new[] { "PokemonId" });
            DropIndex("dbo.TrainerItems", new[] { "TrainerId" });
            DropIndex("dbo.TrainerItems", new[] { "ItemId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Pokemon", new[] { "Trainer_TrainerId" });
            DropIndex("dbo.Pokemon", new[] { "Route_RouteId" });
            DropIndex("dbo.Item", new[] { "Trainer_TrainerId" });
            DropIndex("dbo.Item", new[] { "Route_RouteId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.TrainerPokemon");
            DropTable("dbo.Trainer");
            DropTable("dbo.TrainerItems");
            DropTable("dbo.Route");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Pokemon");
            DropTable("dbo.Item");
        }
    }
}
