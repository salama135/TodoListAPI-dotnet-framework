using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Models;

namespace TodoListAPI.Criteria
{
    public abstract class SearchCriteria<T> where T : BaseEntity
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool IsDesc { get; set; }
        public T Entity { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}