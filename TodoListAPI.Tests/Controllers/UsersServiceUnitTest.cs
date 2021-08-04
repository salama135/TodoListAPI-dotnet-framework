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
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IUserRepository> userRepo;
        private readonly List<User> mockedData = new List<User>()
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
        }; 
        
        public UsersServiceUnitTest()
        {
            AutoMapperConfigure.Register();

            userRepo = new Mock<IUserRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.UserRepository).Returns(() => userRepo.Object);
            mockUnitOfWork.Setup(uow => uow.Save()).Returns(true);

            service = new UsersService(mockUnitOfWork.Object);
        }

        [TestMethod]
        public void Get_shouldReturnUsersSortedByNameDesc_WhenUserExists()
        {
            // Arrange
            string search = null;
            string sort = "Name";
            bool isDesc = false;
            User model = null;
            int userId = -1;
            int pageIndex = 0;
            int pageSize = 0;
            SearchCriteria<User> searchCriteriaForRepo = new SearchCriteria<User>(search, sort, isDesc, model, userId, pageIndex, pageSize);
                
            var sortedMockedData = mockedData.OrderBy(p => p.Name).ToList();

            userRepo.Setup(ur => ur.Get(It.IsAny<BaseSearchCriteria>())).Returns(() => sortedMockedData);
                       
            // Act
            UserDTO dto = null;
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

            var sortedMockedData = mockedData.OrderByDescending(u => u.Name).ToList();

            userRepo.Setup(ur => ur.Get(It.IsAny<BaseSearchCriteria>())).Returns(() => sortedMockedData);

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

        [TestMethod]
        public void PostUser_shouldReturnPostedUser_WhenUserProvided()
        {
            User model = mockedData[0]; 

            // Arrange
            userRepo.Setup(ur => ur.Create(It.IsAny<User>())).Returns(model);

            // Act
            UserDTO dto = AutoMapperConfigure._mapper.Map<UserDTO>(model);
            UserDTO returnedUser = service.Post(dto);

            // Assert
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(dto, returnedUser);
        }

        [TestMethod]
        public void PutUser_shouldReturnUpdatedUser_WhenUserProvided()
        {
            User model = mockedData[0];

            // Arrange
            userRepo.Setup(ur => ur.Update(It.IsAny<User>())).Returns(model);

            // Act
            UserDTO dto = AutoMapperConfigure._mapper.Map<UserDTO>(model);
            UserDTO returnedUser = service.Put(dto);

            // Assert
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(dto, returnedUser);
        }

        [TestMethod]
        public void DeleteUser_shouldReturnTrue_WhenUserExists()
        {
            User model = mockedData[0];

            // Arrange
            userRepo.Setup(ur => ur.Delete(It.IsAny<int>())).Returns(true);

            // Act
            UserDTO dto = AutoMapperConfigure._mapper.Map<UserDTO>(model);
            bool result = service.Delete(model.Id);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
