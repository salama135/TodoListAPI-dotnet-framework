using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAPI.Criteria;
using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public interface ITodoItemsService<T> where T : BaseEntity
    {
        IEnumerable<T> Get(SearchCriteria<T> todoItemSearchCriteria);
        T Post(T todoItem);
        T Put(int id, T todoItem);
        bool Delete(int id);
    }
}
