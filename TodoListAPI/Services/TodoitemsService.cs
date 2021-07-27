using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private ITodoItemRepository _todoItemRepository;

        private MapperConfiguration config;

        public TodoItemsService(ITodoItemRepository employeeRepository)
        {
            _todoItemRepository = employeeRepository;
            ConfigureMapper();
        }

        public void ConfigureMapper()
        {
            config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<TodoItem, TodoItemDTO>()
               );
        }

        public IEnumerable<TodoItemDTO> Get(TodoItemSearchCriteria todoItemSearchCriteria)
        {
            Mapper mapper = new Mapper(config);
            IEnumerable<TodoItem> result;

            if (todoItemSearchCriteria.search != null) result = _todoItemRepository.GetFiltered(todoItemSearchCriteria.search);
            else
            if (todoItemSearchCriteria.sort != null) result = _todoItemRepository.GetSorted(todoItemSearchCriteria.sort, todoItemSearchCriteria.isDescending);
            else
            {
                result = _todoItemRepository.GetAll();
            }

            IEnumerable<TodoItemDTO> resultDTO = mapper.Map<IEnumerable<TodoItemDTO>>(result);

            return resultDTO;
        }

        public TodoItemDTO Get(int id)
        {
            Mapper mapper = new Mapper(config);

            TodoItem result = _todoItemRepository.GetById(id);

            TodoItemDTO resultDTO = mapper.Map<TodoItemDTO>(result);

            return resultDTO;
        }

        public bool Post(TodoItem todoItem)
        {
            if (todoItem == null) return false;

            bool successful = _todoItemRepository.Insert(todoItem);

            try
            {
                if (successful) _todoItemRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return successful;
        }

        public bool Put(int id, TodoItem todoItem)
        {
            bool successful = _todoItemRepository.Update(id, todoItem);

            try
            {
                if (successful) _todoItemRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return successful;
        }

        public bool Delete(int id)
        {
            bool successful = _todoItemRepository.Delete(id);
            try
            {
                if (successful) _todoItemRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return successful;
        }
    }
}