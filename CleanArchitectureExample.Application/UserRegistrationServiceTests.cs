using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Application.Services;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.Application.Dtos;

namespace CleanArchitectureExample.Application
{
    public class UserRegistrationServiceTests
    {
        [Fact]
        public async Task RegisterUserAsync_ReturnsFalse_IfEmailExists()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            var service = new UserRegistrationService(mockRepo.Object);
            var userDto = new UserDto { Name = "TestUser", Email = "test@example.com" };
            var result = await service.RegisterUserAsync(userDto);

            Assert.False(result);
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsTrue_IfRegistrationSucceeds()
        {
            //arrange  
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            var service = new UserRegistrationService(mockRepo.Object);
            var userDto = new UserDto { Name = "ExampleUser", Email = "new@example.com" };
            //act  
            var result = await service.RegisterUserAsync(userDto);
            //assert  
            Assert.True(result);
        }
    }
}