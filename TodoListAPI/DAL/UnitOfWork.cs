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
        public IUserAuthRepository UserAuthRepository { get; set; }

        public UnitOfWork(
            TodoListAPIContext Context, 
            ITodoItemRepository todoItemRepository, 
            IUserRepository userRepository, 
            IUserAuthRepository userAuthRepository)
        {
            _context = Context;
            TodoItemRepository = todoItemRepository;
            UserRepository = userRepository;
            UserAuthRepository = userAuthRepository;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
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