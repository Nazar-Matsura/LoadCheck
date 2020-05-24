namespace LoadCheck.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "TimeOfTest", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TestResults", "MinResponseTime", c => c.Long(nullable: false));
            AlterColumn("dbo.TestResults", "MaxResponseTime", c => c.Long(nullable: false));
            DropColumn("dbo.Tests", "TestStart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "TestStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TestResults", "MaxResponseTime", c => c.Int(nullable: false));
            AlterColumn("dbo.TestResults", "MinResponseTime", c => c.Int(nullable: false));
            DropColumn("dbo.Tests", "TimeOfTest");
        }
    }
}
