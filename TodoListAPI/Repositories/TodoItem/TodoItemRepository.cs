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

    }

    public class TodoItemRepository : CrudRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListAPIContext Context) : base(Context)
        {
        }

        public override IQueryable<TodoItem> Search(BaseSearchCriteria criteria)
        {
            var query = context.TodoItems
                .Where(t => string.IsNullOrEmpty(criteria.Search) || t.Title.Contains(criteria.Search))
                .Where(t => criteria.UserId >= 0 && t.UserId == criteria.UserId)
                .Where(t => (criteria.ItemId >= 0 && t.Id == criteria.ItemId) || (criteria.ItemId < 0));

            // creationDate < 1-1-2020
            // Contains("title")
            // id = 28

            return query;
        }
    }
}