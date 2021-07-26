using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListAPI.Criteria
{
    public class TodoItemSearchCriteria
    {
        public string search { get; set; }

        public string sort { get; set; }

        public bool isDescending { get; set; }

        public TodoItemSearchCriteria(string _search, string _sort, bool _isDescending)
        {
            search = _search;
            sort = _sort;
            isDescending = _isDescending;
        }
    }
}