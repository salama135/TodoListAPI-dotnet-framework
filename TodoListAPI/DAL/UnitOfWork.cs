using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.DAL
{
    public class UnitOfWork
    {
        public CrudRepository<TodoItem> _todoItemRepository { get; }

        public UnitOfWork(TodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public void Save()
        {
            _todoItemRepository.Save();
        }
    }
}