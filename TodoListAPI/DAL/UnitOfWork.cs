using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TodoListAPIContext _context;

        public ITodoItemRepository TodoItemRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(TodoListAPIContext Context, ITodoItemRepository todoItemRepository, IUserRepository userRepository)
        {
            _context = Context;
            TodoItemRepository = todoItemRepository;
            UserRepository = userRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
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