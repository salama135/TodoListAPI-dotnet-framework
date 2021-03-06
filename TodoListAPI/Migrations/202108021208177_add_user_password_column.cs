namespace TodoListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_user_password_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
        }
    }
}
