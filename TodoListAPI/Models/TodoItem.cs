using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoListAPI.Models
{
    public class TodoItem: BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public DateTime CreationData { get; set; }
        public bool IsDone { get; set; } = false;
        public string Description { get; set; }

        public TodoItem()
        {

        }

        public TodoItem(string title, string description, DateTime creationDate, bool isDone)
        {
            Title = title;
            Description = description;
            CreationData = creationDate;
            IsDone = isDone;
        }
    }
}