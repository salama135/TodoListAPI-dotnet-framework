namespace TodoListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_user_model : DbMigration
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TodoItems", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoItems", "UserId");
            DropTable("dbo.Users");
        }
    }
}
