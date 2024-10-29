using api.Controllers;
using api.Models;
using api.Repository;
// using FakeItEasy;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
namespace api.tests.Controller
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserController _controller;
        public UserControllerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _controller = new UserController(_mockRepo.Object);
        }       

        [Fact]
        public async Task UserController_GetUserById_ReturnUser(){
            // Arrange
            var user = new User{
                    Id = 1
            };
            _mockRepo.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(user));
            
            // Act
            var result = await _controller.GetUserById(1);
            
            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UserController_GetUserById_ReturnNotFound(){
            // Arrange
            var user = new User{
                    Id = 1,
            };
            _mockRepo.Setup(x => x.GetUserById(user.Id)).Returns(Task.FromResult(user));
            
            // Act
            var result = (NotFoundResult) await _controller.GetUserById(7);
            
            // Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UserController_GetAllUsers_ReturnListOfUser(){
            // Arrange
            _mockRepo.Setup(x => x.GetAllUsers()).Returns(Task.FromResult(new List<User>()));
        
            // Act
            var result = await _controller.GetAllUsers();
            
            // Assert
            result.Should().NotBeNull();
        }

    //     [Fact]
    //     public async Task UserController_GetAllUsers_ReturnListNotFound(){
    //         // Arrange
    //         _mockRepo.Setup(x => x.GetAllUsers());
    //         // Act

    //         // Assert
    //     } 

        [Fact]
        public async Task UserController_AddUser_ReturnOk(){
            // Arrange
            var user = new User{
                    Id = 1,
                    Username = "farmaan",
                    Email = "farmaan@mail.com",
                    Password = "farmaan123"
            };
            _mockRepo.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await _controller.AddUser(user);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task userController_UpdateUser_returnOk(){
            // Arrange
            var user = new User{
                    Id = 1,
                    Username = "farmaan",
                    Email = "jam@mail.com",
                    Password = "farmaan123"
            };
            _mockRepo.Setup(x => x.UpdateUser(It.IsAny<User>(),user.Id)).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await _controller.UpdateUser(user, 1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task UserController_UpdateUser_returnNotFound(){
            // Arrange
            var user = new User{
                    Id = 1,
                    Username = "farmaan",
                    Email = "jam@mail.com",
                    Password = "farmaan123"
            };
            _mockRepo.Setup(x => x.UpdateUser(It.IsAny<User>(),user.Id)).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await _controller.UpdateUser(user, 7);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UserController_DeleteUser_returnOk(){
            // var user = new User{
            //     Id = 1,
            // };
            // Arrange
            _mockRepo.Setup(x => x.DeleteUser(It.IsAny<int>())).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UserController_LoginUser_returnOk(){
            // Arrange
            var user = new User{
                    Email = "jam@mail.com",
                    Password = "farmaan123"
            };
            _mockRepo.Setup(x => x.LoginUser("jam@mail.com", "farmaan123")).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.LoginUser(user);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task userController_LoginUser_ReturnNOtFound(){
            // Arrange
            var user = new User{
                    Email = "jam@mail.com",
                    Password = "farmaan123"
            };
            _mockRepo.Setup(x => x.LoginUser("jam@mail.com", "farmaan13")).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.LoginUser(user);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

    }
}