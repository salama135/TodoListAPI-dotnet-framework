using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TodoListAPI.Repositories;
using TodoListAPI.Resolvers;
using TodoListAPI.Services;
using Unity;
using Unity.Lifetime;

namespace TodoListAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //var container = new UnityContainer();
            //container.RegisterType<ITodoItemRepository, TodoItemRepository>();
            //container.RegisterType<ITodoitemsService, TodoitemsService>();
            //config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
