using AutoMapper;
using System;
using TodoListAPI.AutoMapperConfig;
using TodoListAPI.DAL;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;
using TodoListAPI.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoListAPI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            //ContainerControlledTransientManager or ContainerControlledLifetimeManager
            container.RegisterType<TodoListAPIContext>(new ContainerControlledLifetimeManager());

            //var context = container.Resolve<TodoListAPIContext>();
            //container.RegisterType<ICrudRepository<TodoItem>, TodoItemRepository>(new InjectionConstructor(new object [] { context }));

            container.RegisterType<ITodoItemRepository, TodoItemRepository>();
            container.RegisterType<IUnitOfWork<TodoItem>, UnitOfWork>();
            container.RegisterType<IService<TodoItemDTO>, TodoItemsService>();


            AutoMapperConfigure.Register();
        }
    }
}