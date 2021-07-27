using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Repositories;

namespace TodoListAPI.Data
{
    public class TodoListAPI_DB_UnitOfWork
    {
        private readonly TodoListAPIContext _context;

        private ITodoItemRepository _todoItemRepository;

        public TodoListAPI_DB_UnitOfWork(TodoListAPIContext context, TodoItemRepository todoItemRepository)
        {
            _context = context;
            _todoItemRepository = todoItemRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}