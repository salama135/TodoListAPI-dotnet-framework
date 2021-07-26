using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoListAPI.Models
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool isDone { get; set; } = false;
    }
}