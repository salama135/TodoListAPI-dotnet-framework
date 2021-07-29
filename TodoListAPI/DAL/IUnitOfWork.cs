using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAPI.Criteria;
using TodoListAPI.Models;

namespace TodoListAPI.DAL
{
    public interface IUnitOfWork<T> where T : BaseEntity
    {
        IEnumerable<T> Read(SearchCriteria<T> searchCriteria);

        T Create(T item);

        T Update(T Item);

        bool Delete(int id);
    }
}
