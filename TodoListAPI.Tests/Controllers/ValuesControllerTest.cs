using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TodoListAPI;
using TodoListAPI.Controllers;
using System.Data.Entity;
using TodoListAPI.Data;
using TodoListAPI.Services;
using TodoListAPI.Models;
using System.Data.Entity.Migrations;
using Moq;
using TodoListAPI.Repositories;
using TodoListAPI.DAL;
using TodoListAPI.AutoMapperConfig;

namespace TodoListAPI.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            AutoMapperConfigure.Register();

            Mock<ICrudRepository<TodoItem>> mock = new Mock<ICrudRepository<TodoItem>>();

            TodoItem item = new TodoItem("task 1", "finish unit test", new DateTime(1, 1, 1, 1, 1, 1), false);

            mock.Setup(r => r.Create(It.IsAny<TodoItem>())).Returns(item);

            TodoItemsService service = new TodoItemsService(new TodoItemUnitOfWork(null, mock.Object));

            TodoItemDTO itemDTO = AutoMapperConfigure._mapper.Map<TodoItemDTO>(item);

            var dto = service.Post(itemDTO);

            Assert.IsNotNull(dto);
            Assert.AreEqual(itemDTO, dto);

            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
