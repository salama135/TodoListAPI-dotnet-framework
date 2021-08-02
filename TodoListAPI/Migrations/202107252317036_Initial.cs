namespace TodoListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.TodoItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    CreationData = c.DateTime(nullable: false),
                    isDone = c.Boolean(nullable: false),
                    Description = c.String(),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);            
        }
        
        public override void Down()
        {
            DropTable("dbo.TodoItems");
        }
    }
}
