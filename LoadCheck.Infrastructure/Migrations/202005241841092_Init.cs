namespace LoadCheck.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteRoots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Authority = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TestStart = c.DateTime(nullable: false),
                        SiteRootId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteRoots", t => t.SiteRootId, cascadeDelete: true)
                .Index(t => t.SiteRootId);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(),
                        MinResponseTime = c.Int(nullable: false),
                        MaxResponseTime = c.Int(nullable: false),
                        TestId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "SiteRootId", "dbo.SiteRoots");
            DropIndex("dbo.TestResults", new[] { "TestId" });
            DropIndex("dbo.Tests", new[] { "SiteRootId" });
            DropTable("dbo.TestResults");
            DropTable("dbo.Tests");
            DropTable("dbo.SiteRoots");
        }
    }
}
