using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Models;

namespace TodoListAPI.Criteria
{
    public class TodoItemSearchCriteria : SearchCriteria<TodoItem> 
    {
        public TodoItemSearchCriteria(string _search, string _sort, bool _isDescending, TodoItem todoItem, int pageIndex, int pageSize)
        {
            Search = _search;
            SortBy = _sort;
            IsDesc = _isDescending;
            Entity = todoItem;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}