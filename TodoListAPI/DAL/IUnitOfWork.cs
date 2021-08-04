using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.DAL
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoItemRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IUserAuthRepository UserAuthRepository { get; set; }

        bool Save();
    }
}
