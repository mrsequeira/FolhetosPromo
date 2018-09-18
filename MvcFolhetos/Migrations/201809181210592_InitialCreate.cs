namespace MvcFolhetos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Folhetos",
                c => new
                    {
                        FolhetosID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        Pasta = c.String(),
                        DataInic = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                        NomeEmpresa = c.String(),
                    })
                .PrimaryKey(t => t.FolhetosID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Folhetos");
        }
    }
}
