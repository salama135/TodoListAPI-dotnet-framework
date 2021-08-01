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
    public interface ITodoItemRepository : ICrudRepository<TodoItem>
    {
        TodoItem GetByID(int id);
    }

    public class TodoItemRepository : CrudRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListAPIContext Context) : base(Context)
        {
        }

        public override IQueryable<TodoItem> Search(SearchCriteria<TodoItem> criteria)
        {
            var query = context.TodoItems.Where(t => string.IsNullOrEmpty(criteria.Search) ||
                                                        t.Title.Contains(criteria.Search));
            // creationDate < 1-1-2020
            // Contains("title")
            // id = 28


            return query;
        }

        public TodoItem GetByID(int id)
        {
            return context.TodoItems.Find(id);
        }
    }
}