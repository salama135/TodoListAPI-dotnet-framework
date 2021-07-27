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

        public TodoListAPI_DB_UnitOfWork(TodoListAPIContext context)
        {
            _context = context;
        }

        public ITodoItemRepository todoItemRepository
        {
            get
            {
                if(_todoItemRepository == null)
                {
                    _todoItemRepository = new TodoItemRepository(_context);
                }

                return _todoItemRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}