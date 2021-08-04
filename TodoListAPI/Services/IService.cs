using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAPI.Criteria;
using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public interface IService<T> where T : BaseEntity
    {
        IEnumerable<T> Get(SearchCriteria<T> searchCriteria);
        T Post(T dto);
        T Put(T dto);
        bool Delete(int id);
    }
}
