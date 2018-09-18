namespace MvcFolhetos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewActions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Folhetos", "Titulo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Folhetos", "NomeEmpresa", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Folhetos", "NomeEmpresa", c => c.String());
            AlterColumn("dbo.Folhetos", "Titulo", c => c.String());
        }
    }
}
