using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.AutoMapperConfig;
using TodoListAPI.DAL;
using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public interface IUserAuthService
    {
        bool AuthUser(UserDTO dto);

        int GetUserId(UserDTO dto);
    }

    public class UserAuthService : IUserAuthService
    {
        private IUnitOfWork _unitOfWork;

        readonly IMapper mapper = AutoMapperConfigure._mapper;

        public UserAuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AuthUser(UserDTO dto)
        {
            User user = mapper.Map<User>(dto);

            return _unitOfWork.UserAuthRepository.ValidateUser(user) != null;
        }

        public int GetUserId(UserDTO dto)
        {
            User user = mapper.Map<User>(dto);

            return _unitOfWork.UserAuthRepository.ValidateUser(user).Id;
        }
    }
}