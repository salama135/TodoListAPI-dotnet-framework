namespace TodoListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittodoitem : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TodoItems", "UserId");
            AddForeignKey("dbo.TodoItems", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoItems", "UserId", "dbo.Users");
            DropIndex("dbo.TodoItems", new[] { "UserId" });
        }
    }
}
