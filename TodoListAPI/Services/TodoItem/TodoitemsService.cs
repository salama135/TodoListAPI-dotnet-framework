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
    public class TodoItemsService : IService<TodoItemDTO>
    {
        private IUnitOfWork _unitOfWork;

        readonly IMapper mapper = AutoMapperConfigure._mapper;

        public TodoItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TodoItemDTO> Get(BaseSearchCriteria searchCriteria)
        {
            IEnumerable<TodoItem> result;

            result = _unitOfWork.TodoItemRepository.Get(searchCriteria);
            
            IEnumerable<TodoItemDTO> resultDTO = mapper.Map<IEnumerable<TodoItemDTO>>(result);

            return resultDTO;
        }


        public TodoItemDTO Post(TodoItemDTO dto)
        {
            if (dto == null) return null;

            TodoItem model = mapper.Map<TodoItem>(dto);

            bool exists = _unitOfWork.UserRepository.EnteryExists(dto.UserId);

            if (exists == false) return null; 

            TodoItem createdModel = _unitOfWork.TodoItemRepository.Create(model);

            bool successful = (createdModel != null);

            TryToSave(ref successful);

            if (successful == false) return null;

            return dto;
        }

        public TodoItemDTO Put(TodoItemDTO dto)
        {
            if (dto == null) return null;

            TodoItem model = mapper.Map<TodoItem>(dto);

            bool exists = _unitOfWork.UserRepository.EnteryExists(dto.UserId);

            if (exists == false) return null;

            TodoItem editedModel = _unitOfWork.TodoItemRepository.Update(model);

            bool successful = (editedModel != null);

            TryToSave(ref successful);

            if (successful == false) return null;

            return dto;
        }

        public bool Delete(int id)
        {
            bool successful = _unitOfWork.TodoItemRepository.Delete(id);

            TryToSave(ref successful);

            return successful;
        }

        public void TryToSave(ref bool successful)
        {
            try
            {
                if (successful)
                    successful = _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                successful = false;
            }
        }
    }
}