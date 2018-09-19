namespace MvcFolhetos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Info = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagsFolhetos",
                c => new
                    {
                        Tags_ID = c.Int(nullable: false),
                        Folhetos_FolhetosID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_ID, t.Folhetos_FolhetosID })
                .ForeignKey("dbo.Tags", t => t.Tags_ID, cascadeDelete: true)
                .ForeignKey("dbo.Folhetos", t => t.Folhetos_FolhetosID, cascadeDelete: true)
                .Index(t => t.Tags_ID)
                .Index(t => t.Folhetos_FolhetosID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagsFolhetos", "Folhetos_FolhetosID", "dbo.Folhetos");
            DropForeignKey("dbo.TagsFolhetos", "Tags_ID", "dbo.Tags");
            DropIndex("dbo.TagsFolhetos", new[] { "Folhetos_FolhetosID" });
            DropIndex("dbo.TagsFolhetos", new[] { "Tags_ID" });
            DropTable("dbo.TagsFolhetos");
            DropTable("dbo.Tags");
        }
    }
}
