namespace TodoListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                        ParentId = c.Int(nullable: false),
                        Parent_TestId = c.Int(),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.Tests", t => t.Parent_TestId)
                .Index(t => t.Parent_TestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "Parent_TestId", "dbo.Tests");
            DropIndex("dbo.Tests", new[] { "Parent_TestId" });
            DropTable("dbo.Tests");
        }
    }
}
