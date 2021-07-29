using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAPI.Criteria;
using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public interface ITodoItemsService
    {
        IEnumerable<TodoItemDTO> Get(TodoItemSearchCriteria todoItemSearchCriteria);
        TodoItemDTO Post(TodoItemDTO todoItem);
        TodoItemDTO Put(int id, TodoItemDTO todoItem);
        bool Delete(int id);
    }
}
