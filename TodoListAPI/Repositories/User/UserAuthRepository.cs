using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public interface IUserAuthRepository
    {
        User ValidateUser(User user);
    }

    public class UserAuthRepository : IUserAuthRepository
    {
        protected TodoListAPIContext context;

        public UserAuthRepository(TodoListAPIContext Context)
        {
            context = Context;
        }

        public User ValidateUser(User user)
        {
            return context.Users.FirstOrDefault(u =>
            u.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase)
            && u.Password == user.Password);
        }
    }
}