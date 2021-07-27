using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TodoListAPI.AutoMapperConfig;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private ITodoItemRepository _todoItemRepository;

        readonly IMapper mapper = AutoMapperConfigure._mapper;

        public TodoItemsService(ITodoItemRepository employeeRepository)
        {
            _todoItemRepository = employeeRepository;
        }

        public IEnumerable<TodoItemDTO> Get(TodoItemSearchCriteria todoItemSearchCriteria)
        {



            IEnumerable<TodoItem> result;

            if (todoItemSearchCriteria.search != null) result = _todoItemRepository.GetFiltered(todoItemSearchCriteria.search);
            else
            if (todoItemSearchCriteria.sort != null) result = _todoItemRepository.GetSorted(todoItemSearchCriteria.sort, todoItemSearchCriteria.isDescending);
            else
            {
                result = _todoItemRepository.GetAll();
            }

            IEnumerable<TodoItemDTO> resultDTO = this.mapper.Map<IEnumerable<TodoItemDTO>>(result);

            return resultDTO;
        }

        public TodoItemDTO Get(int id)
        {
            TodoItem result = _todoItemRepository.GetById(id);

            TodoItemDTO resultDTO = this.mapper.Map<TodoItemDTO>(result);

            return resultDTO;
        }

        public bool Post(TodoItem todoItem)
        {
            if (todoItem == null) return false;

            bool successful = _todoItemRepository.Insert(todoItem);

            TryToSave(ref successful);

            return successful;
        }

        public bool Put(int id, TodoItem todoItem)
        {
            bool successful = _todoItemRepository.Update(id, todoItem);

            TryToSave(ref successful);

            return successful;
        }

        public bool Delete(int id)
        {
            bool successful = _todoItemRepository.Delete(id);

            TryToSave(ref successful);

            return successful;
        }

        public void TryToSave(ref bool successful)
        {
            try
            {
                if (successful) _todoItemRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                successful = false;
            }
        }

    }
}