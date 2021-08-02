using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoListAPI.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}