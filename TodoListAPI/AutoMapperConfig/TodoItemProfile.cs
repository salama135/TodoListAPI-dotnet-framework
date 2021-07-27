using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Models;

namespace TodoListAPI.AutoMapperConfig
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}