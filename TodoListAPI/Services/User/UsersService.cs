using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TodoListAPI.AutoMapperConfig;
using TodoListAPI.Criteria;
using TodoListAPI.DAL;
using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public class UsersService : IService<UserDTO>
    {
        private IUnitOfWork _unitOfWork;

        readonly IMapper mapper = AutoMapperConfigure._mapper;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserDTO> Get(BaseSearchCriteria searchCriteria)
        {
            IEnumerable<User> result;

            result = _unitOfWork.UserRepository.Get(searchCriteria);

            IEnumerable<UserDTO> resultDTO = mapper.Map<IEnumerable<UserDTO>>(result);

            return resultDTO;
        }

        //T getType(LevelDTO)
        //{
        //    return mapper.Map<T>(dto);
        //}

        public UserDTO Post(UserDTO dto)
        {
            if (dto == null) return null;

            User model = mapper.Map<User>(dto);

            // Room room = mapper.Map<Room>(dto);

            User createdModel = _unitOfWork.UserRepository.Create(model);

            bool successful = (createdModel != null);

            TryToSave(ref successful);

            if (successful == false) return null;

            return dto;
        }

        public UserDTO Put(UserDTO dto)
        {
            if (dto == null) return null;

            User model = mapper.Map<User>(dto);

            User editedModel = _unitOfWork.UserRepository.Update(model);

            bool successful = (editedModel != null);

            TryToSave(ref successful);

            if (successful == false) return null;

            return dto;
        }

        public bool Delete(int id)
        {
            bool successful = _unitOfWork.UserRepository.Delete(id);

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