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
        [Required]
        public string Password { get; set; }

        public User()
        {

        }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}