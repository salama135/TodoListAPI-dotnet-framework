using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAPI.Criteria;
using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public interface ITodoitemsService
    {
        IEnumerable<TodoItemDTO> Get(TodoItemSearchCriteria todoItemSearchCriteria);
        TodoItemDTO Get(int id);
        bool Post(TodoItem todoItem);
        bool Put(int id, TodoItem todoItem);
        bool Delete(int id);
    }
}
