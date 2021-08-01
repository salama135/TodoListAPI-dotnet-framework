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
    public interface IUnitOfWork<T> where T : BaseEntity
    {
        ITodoItemRepository TodoItemRepository { get; set; }

        void Save();
    }
}
