namespace MvcFolhetos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizadores", "UserName", c => c.String());
            DropColumn("dbo.Utilizadores", "NomeRegistoDoUtilizador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilizadores", "NomeRegistoDoUtilizador", c => c.String());
            DropColumn("dbo.Utilizadores", "UserName");
        }
    }
}
