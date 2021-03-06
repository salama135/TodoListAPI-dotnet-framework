using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoListAPI.Models
{
    public class UserDTO : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

        public UserDTO()
        {

        }

        public UserDTO(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}