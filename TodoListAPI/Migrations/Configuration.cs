namespace TodoListAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TodoListAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoListAPI.Data.TodoListAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TodoListAPI.Data.TodoListAPIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            context.TodoItems.AddOrUpdate(
              p => p.Id,
              new TodoItem { Id = 1, Title = "task 1", Description = "this is a new task 1", CreationData = new DateTime(2021, 8, 15, 1, 1, 1) },
              new TodoItem { Id = 2, Title = "task 2", Description = "this is a new task 2", CreationData = new DateTime(2021, 8, 15, 1, 2, 1) },
              new TodoItem { Id = 3, Title = "task 3", Description = "this is a new task 3", CreationData = new DateTime(2021, 8, 15, 1, 3, 1) }
            );

        }
    }
}
