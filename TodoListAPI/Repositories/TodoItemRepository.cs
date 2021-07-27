using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoListAPI.Data;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoListAPIContext _context;

        
        public TodoItemRepository(TodoListAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem GetById(int todoItemID)
        {
            return _context.TodoItems.Find(todoItemID);
        }

        public bool Insert(TodoItem todoItem)
        {
            TodoItem newTodoItem = _context.TodoItems.Add(todoItem);

            if (newTodoItem == null) return false;

            return true;
        }

        public bool Update(int todoItemID, TodoItem todoItem)
        {
            bool itemExists = TodoItemExists(todoItemID);

            if (itemExists == false) return false;

            _context.Entry(todoItem).State = EntityState.Modified;

            return true;
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Count(e => e.Id == id) > 0;
        }

        public bool Delete(int todoItemID)
        {
            TodoItem todoItem = _context.TodoItems.Find(todoItemID);

            if (todoItem == null) return false;

            _context.TodoItems.Remove(todoItem);

            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        public IEnumerable<TodoItem> GetSorted(string sortBy, bool isDesc)
        {
            ChooseGetSorted handler = GetSortedByTitle;
            IEnumerable<TodoItem> result = null;

            if (sortBy == "CreationDate")
            {
                handler = GetSortedByCreationData;
            }
            else
            if (sortBy == "Title")
            {
                handler = GetSortedByTitle;
            }

            result = handler(_context, isDesc);
            return result;
        }

        private static IEnumerable<TodoItem> GetSortedByTitle(TodoListAPIContext _context, bool isDesc)
        {
            return (isDesc) ?
                _context.TodoItems.OrderByDescending(t => t.Title).ToList() :
                _context.TodoItems.OrderBy(t => t.Title).ToList();
        }

        private static IEnumerable<TodoItem> GetSortedByCreationData(TodoListAPIContext _context, bool isDesc)
        {
            return (isDesc) ?
                _context.TodoItems.OrderByDescending(t => t.CreationData).ToList() :
                _context.TodoItems.OrderBy(t => t.CreationData).ToList();
        }
        
        private delegate IEnumerable<TodoItem> ChooseGetSorted(TodoListAPIContext _context, bool isDesc);

        public IEnumerable<TodoItem> GetFiltered(string filterBy)
        {
            var todoes = from t in _context.TodoItems where t.Title.Contains(filterBy) select t;

            return todoes.ToList();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}