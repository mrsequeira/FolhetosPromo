namespace MvcFolhetos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
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
                        FolhetosID = c.Int(nullable: false, identity: true),
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
                "dbo.Utilizadores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomeProprio = c.String(nullable: false),
                        Apelido = c.String(),
                        NomeRegistoDoUtilizador = c.String(),
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
            DropForeignKey("dbo.TagsFolhetos", "Folhetos_FolhetosID", "dbo.Folhetos");
            DropForeignKey("dbo.TagsFolhetos", "Tags_ID", "dbo.Tags");
            DropForeignKey("dbo.FolhetosCategorias", "Categorias_ID", "dbo.Categorias");
            DropForeignKey("dbo.FolhetosCategorias", "Folhetos_FolhetosID", "dbo.Folhetos");
            DropIndex("dbo.TagsFolhetos", new[] { "Folhetos_FolhetosID" });
            DropIndex("dbo.TagsFolhetos", new[] { "Tags_ID" });
            DropIndex("dbo.FolhetosCategorias", new[] { "Categorias_ID" });
            DropIndex("dbo.FolhetosCategorias", new[] { "Folhetos_FolhetosID" });
            DropTable("dbo.TagsFolhetos");
            DropTable("dbo.FolhetosCategorias");
            DropTable("dbo.Utilizadores");
            DropTable("dbo.Tags");
            DropTable("dbo.Folhetos");
            DropTable("dbo.Categorias");
        }
    }
}
