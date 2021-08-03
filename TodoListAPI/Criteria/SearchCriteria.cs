using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Models;

namespace TodoListAPI.Criteria
{
    public class BaseSearchCriteria
    {
        public BaseSearchCriteria(string _search, string _sort, bool _isDescending, int userId, int pageIndex, int pageSize)
        {
            Search = _search;
            SortBy = _sort;
            IsDesc = _isDescending;
            UserId = userId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool IsDesc
        {
            get; set;
        }
        public int UserId { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public BaseSearchCriteria GetObject()
        {
            return this;
        }
    }
    public class SearchCriteria<T> : BaseSearchCriteria where T : BaseEntity
    {

        public T Entity { get; set; }

        public SearchCriteria(string _search, string _sort, bool _isDescending, T entity, int userId, int pageIndex, int pageSize)
            : base(_search, _sort, _isDescending, userId, pageIndex, pageSize)
        {
            Entity = entity;
        }
    }
}