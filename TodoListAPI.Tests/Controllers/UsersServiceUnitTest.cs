using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoListAPI.AutoMapperConfig;
using TodoListAPI.Criteria;
using TodoListAPI.DAL;
using TodoListAPI.Extension;
using TodoListAPI.Models;
using TodoListAPI.Repositories;
using TodoListAPI.Services;

namespace TodoListAPI.Tests.Controllers
{
    [TestClass]
    public class UsersServiceUnitTest
    {
        private readonly UsersService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        private readonly IQueryable<User> mockedData = Enumerable.Empty<User>().AsQueryable<User>();        
        
        public UsersServiceUnitTest()
        {
            AutoMapperConfigure.Register();
            SetupData();

            unitOfWork = new UnitOfWork(null, null, userRepo.Object, null);
            service = new UsersService(unitOfWork);
        }

        private void SetupData()
        {
            mockedData.Concat(new[]
            {
                new User
                {
                    Id = 1,
                    Name = "ahmed",
                    Password = "123"
                },
                new User
                {
                    Id = 2,
                    Name = "mohamed",
                    Password = "1234"
                },
                new User
                {
                    Id = 3,
                    Name = "ali",
                    Password = "12"
                },
                new User
                {
                    Id = 4,
                    Name = "shehab",
                    Password = "333"
                },
                new User
                {
                    Id = 5,
                    Name = "salah",
                    Password = "111"
                },
                new User
                {
                    Id = 6,
                    Name = "abdullah",
                    Password = "222"
                }
            });
        }

        [TestMethod]
        public void Get_shouldReturnUsersSortedByNameDesc_WhenUserExists()
        {
            // Arrange
            string search = null;
            string sort = "Name";
            bool isDesc = false;
            UserDTO dto = It.IsAny<UserDTO>();
            int userId = -1;
            int pageIndex = 0;
            int pageSize = 0;

            mockedData.OrderByDynamically(sort, isDesc);
            var sortedMockedData = mockedData.ToList();

            userRepo.Setup(ur => ur.Read(It.IsAny<SearchCriteria<User>>())).Returns(() => sortedMockedData);

            // Act
            SearchCriteria<UserDTO> criteriaForSrvice = new SearchCriteria<UserDTO>(search, sort, isDesc, dto, userId, pageIndex, pageSize);
            List<UserDTO> returnedUsers = service.Get(criteriaForSrvice).ToList();

            // Assert
            Assert.IsNotNull(returnedUsers);
            Assert.AreEqual(returnedUsers.Count, sortedMockedData.Count);

            for (int i = 0; i < returnedUsers.Count; i++)
            {
                Assert.AreEqual(returnedUsers[i].Id, sortedMockedData[i].Id);
                Assert.AreEqual(returnedUsers[i].Name, sortedMockedData[i].Name);
                Assert.AreEqual(returnedUsers[i].Password, sortedMockedData[i].Password);
            }
        }

        [TestMethod]
        public void Get_shouldReturnUsersSortedByNameAsec_WhenUserExists()
        {
            // Arranges
            string search = null;
            string sort = "Name";
            bool isDesc = true;
            UserDTO dto = It.IsAny<UserDTO>();
            int userId = -1;
            int pageIndex = 0;
            int pageSize = 0;

            mockedData.OrderByDynamically(sort, isDesc);
            var sortedMockedData = mockedData.ToList();

            userRepo.Setup(ur => ur.Read(It.IsAny<SearchCriteria<User>>())).Returns(() => sortedMockedData);

            // Act
            SearchCriteria<UserDTO> criteriaForSrvice = new SearchCriteria<UserDTO>(search, sort, isDesc, dto, userId, pageIndex, pageSize);
            List<UserDTO> returnedUsers = service.Get(criteriaForSrvice).ToList();

            // Assert
            Assert.IsNotNull(returnedUsers);
            Assert.AreEqual(returnedUsers.Count, sortedMockedData.Count);

            for (int i = 0; i < returnedUsers.Count; i++)
            {
                Assert.AreEqual(returnedUsers[i].Id, sortedMockedData[i].Id);
                Assert.AreEqual(returnedUsers[i].Name, sortedMockedData[i].Name);
                Assert.AreEqual(returnedUsers[i].Password, sortedMockedData[i].Password);
            }
        }

        [TestMethod]
        public void GetByID_shouldReturnUser_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            string userName = "Ahmed";
            string userPassword = "1234";

            User user = new User
            {
                Id = userId,
                Name = userName,
                Password = userPassword
            };

            userRepo.Setup(ur => ur.GetByID(userId)).Returns(user);

            // Act
            UserDTO returnedUser = service.GetByID(userId);

            // Assert
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(returnedUser.Id, userId);
            Assert.AreEqual(returnedUser.Name, userName);
            Assert.AreEqual(returnedUser.Password, userPassword);
        }

        [TestMethod]
        public void GetByID_shouldReturnNothing_WhenUserDoesnotExists()
        {
            int userId = -1;

            // Arrange
            userRepo.Setup(ur => ur.GetByID(It.IsAny<int>())).Returns(() => null);

            // Act
            UserDTO returnedUser = service.GetByID(userId);

            // Assert
            Assert.IsNull(returnedUser);
        }
    }
}
