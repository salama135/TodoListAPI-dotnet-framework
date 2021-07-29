using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public class TodoItemRepository : CrudRepository<TodoItem>
    {
        public TodoItemRepository(TodoListAPIContext Context) : base(Context)
        {
        }

        public override IQueryable<TodoItem> Search(SearchCriteria<TodoItem> criteria)
        {
            var query = context.TodoItems.Where(t => string.IsNullOrEmpty(criteria.Search) ||
                                                        t.Title.Contains(criteria.Search));

            return query;
        }

        public override TodoItem GetByID(int id)
        {
            return context.TodoItems.Find(id);
        }

        public override void Save()
        {
            context.SaveChanges();
        }
    }
}