using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Models;

namespace TodoListAPI.AutoMapperConfig
{
    public class AutoMapperConfigure
    {
        public static IMapper _mapper { get; set; }

        public static void Register()
        {
            var mapperConfig = new MapperConfiguration(
                config =>
                {
                    config.AddProfile<TodoItemProfile>();
                    config.AddProfile<UserProfile>();
                }
            );

            _mapper = mapperConfig.CreateMapper();
        }
    }
}