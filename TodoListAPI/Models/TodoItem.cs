using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoListAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreationData { get; set; }
        public bool isDone { get; set; } = false;
        public string Description { get; set; }
    }
}