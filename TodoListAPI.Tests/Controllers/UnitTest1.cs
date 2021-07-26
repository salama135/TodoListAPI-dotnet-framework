using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TodoListAPI.Controllers;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Services;

namespace TodoListAPI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Should_Get_All_TodoItems_In_DB()
        {
            //// Arrange
            //TodoitemsService service = new TodoitemsService();
            //TodoItemSearchCriteria todoItemSearchCriteria = new TodoItemSearchCriteria(null, null, false);

            //// Act
            //IEnumerable<TodoItemDTO> result = service.Get(todoItemSearchCriteria);

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(4, result.Count());
        }
    }
}
