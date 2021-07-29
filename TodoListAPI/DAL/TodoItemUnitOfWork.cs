using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.DAL
{
    public class TodoItemUnitOfWork : IUnitOfWork<TodoItem>
    {
        public CrudRepository<TodoItem> _todoItemRepository { get; }

        public TodoItemUnitOfWork(TodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public void Save()
        {
            _todoItemRepository.Save();
        }

        public TodoItem GetByID(int id)
        {
            return _todoItemRepository.GetByID(id);
        }

        public IEnumerable<TodoItem> Read(SearchCriteria<TodoItem> searchCriteria)
        {
            return _todoItemRepository.Read(searchCriteria);
        }

        public TodoItem Create(TodoItem item)
        {
            return _todoItemRepository.Create(item);
        }

        public TodoItem Update(TodoItem item)
        {
            return _todoItemRepository.Update(item);
        }

        public bool Delete(int id)
        {
            return _todoItemRepository.Delete(id);
        }
    }
}