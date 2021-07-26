using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public interface ITodoItemRepository
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(int todoItemID);
        bool Insert(TodoItem todoItem);
        bool Update(int todoItemID, TodoItem todoItem);
        bool Delete(int todoItemID);
        void Save();

        IEnumerable<TodoItem> GetSorted(string sortBy, bool isDesc);
        IEnumerable<TodoItem> GetFiltered(string filterBy);
    }
}