namespace MvcFolhetos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getID : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Folhetos",
                c => new
                    {
                        FolhetosID = c.Int(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(),
                        DataInic = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                        NomeEmpresa = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FolhetosID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Info = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NomeProprio = c.String(),
                        Apelido = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Utilizadores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomeProprio = c.String(nullable: false),
                        Apelido = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FolhetosCategorias",
                c => new
                    {
                        Folhetos_FolhetosID = c.Int(nullable: false),
                        Categorias_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Folhetos_FolhetosID, t.Categorias_ID })
                .ForeignKey("dbo.Folhetos", t => t.Folhetos_FolhetosID)
                .ForeignKey("dbo.Categorias", t => t.Categorias_ID)
                .Index(t => t.Folhetos_FolhetosID)
                .Index(t => t.Categorias_ID);
            
            CreateTable(
                "dbo.TagsFolhetos",
                c => new
                    {
                        Tags_ID = c.Int(nullable: false),
                        Folhetos_FolhetosID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_ID, t.Folhetos_FolhetosID })
                .ForeignKey("dbo.Tags", t => t.Tags_ID)
                .ForeignKey("dbo.Folhetos", t => t.Folhetos_FolhetosID)
                .Index(t => t.Tags_ID)
                .Index(t => t.Folhetos_FolhetosID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TagsFolhetos", "Folhetos_FolhetosID", "dbo.Folhetos");
            DropForeignKey("dbo.TagsFolhetos", "Tags_ID", "dbo.Tags");
            DropForeignKey("dbo.FolhetosCategorias", "Categorias_ID", "dbo.Categorias");
            DropForeignKey("dbo.FolhetosCategorias", "Folhetos_FolhetosID", "dbo.Folhetos");
            DropIndex("dbo.TagsFolhetos", new[] { "Folhetos_FolhetosID" });
            DropIndex("dbo.TagsFolhetos", new[] { "Tags_ID" });
            DropIndex("dbo.FolhetosCategorias", new[] { "Categorias_ID" });
            DropIndex("dbo.FolhetosCategorias", new[] { "Folhetos_FolhetosID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.TagsFolhetos");
            DropTable("dbo.FolhetosCategorias");
            DropTable("dbo.Utilizadores");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.Folhetos");
            DropTable("dbo.Categorias");
        }
    }
}
