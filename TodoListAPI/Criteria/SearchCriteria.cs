using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Models;

namespace TodoListAPI.Criteria
{
    public class SearchCriteria<T> where T : BaseEntity
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool IsDesc { get; set; }
        public T Entity { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public SearchCriteria(string _search, string _sort, bool _isDescending, T todoItem, int pageIndex, int pageSize)
        {
            Search = _search;
            SortBy = _sort;
            IsDesc = _isDescending;
            Entity = todoItem;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}