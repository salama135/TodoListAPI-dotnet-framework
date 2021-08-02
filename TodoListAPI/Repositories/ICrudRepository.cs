﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListAPI.Criteria;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        T Create(T item);
        
        IList<T> Read(SearchCriteria<T> criteria);

        T Update(T Item);

        bool Delete(int id);
    }
}
