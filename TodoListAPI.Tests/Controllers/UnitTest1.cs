using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using TodoListAPI.Controllers;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Repositories;
using TodoListAPI.Services;

namespace TodoListAPI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        private TodoItemsService service;
        private Mock<ITodoItemsService> todoItemsServiceServiceMock;

        [SetUp]
        public void Setup()
        {

        }

        [TestMethod]
        public void Should_Get_All_TodoItems_In_DB()
        {
            // Arrange
            var todoItemsServiceMock = new Mock<ITodoItemsService>();
            TodoItemSearchCriteria todoItemSearchCriteria = new TodoItemSearchCriteria(null, null, false);
        }
    }
}
