using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TodoListAPI.AutoMapperConfig;
using TodoListAPI.Criteria;
using TodoListAPI.DAL;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private IUnitOfWork<TodoItem> _unitOfWork;

        readonly IMapper mapper = AutoMapperConfigure._mapper;

        public TodoItemsService(TodoItemUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TodoItemDTO> Get(TodoItemSearchCriteria todoItemSearchCriteria)
        {
            IEnumerable<TodoItem> result;

            result = _unitOfWork.Read(todoItemSearchCriteria);

            IEnumerable<TodoItemDTO> resultDTO = mapper.Map<IEnumerable<TodoItemDTO>>(result);

            return resultDTO;
        }

        public TodoItemDTO GetByID(int id)
        {
            TodoItem result;

            result = ((TodoItemUnitOfWork)_unitOfWork).GetByID(id);

            TodoItemDTO resultDTO = mapper.Map<TodoItemDTO>(result);

            return resultDTO;
        }

        public TodoItemDTO Post(TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO == null) return null;

            TodoItem todoItem = mapper.Map<TodoItem>(todoItemDTO);

            TodoItem newTodoItem = _unitOfWork.Create(todoItem);

            bool successful = (newTodoItem != null);

            TryToSave(ref successful);

            if (successful == false) return null;

            return todoItemDTO;
        }

        public TodoItemDTO Put(int id, TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO == null) return null;

            TodoItem todoItem = mapper.Map<TodoItem>(todoItemDTO);

            TodoItem newTodoItem = _unitOfWork.Update(todoItem);

            bool successful = (newTodoItem != null);

            TryToSave(ref successful);

            if (successful == false) return null;

            return todoItemDTO;
        }

        public bool Delete(int id)
        {
            bool successful = _unitOfWork.Delete(id);

            TryToSave(ref successful);

            return successful;
        }

        public void TryToSave(ref bool successful)
        {
            try
            {
                if (successful) _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                successful = false;
            }
        }
    }
}