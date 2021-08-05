using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoListAPI.Models
{
    public class Test 
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int ParentId { get; set; }
        public Test Parent { get; set; }
    }
}