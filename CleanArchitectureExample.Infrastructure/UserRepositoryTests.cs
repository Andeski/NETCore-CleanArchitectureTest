using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.ValueObjects;
using CleanArchitectureExample.Infrastructure.Data;
using CleanArchitectureExample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitectureExample.Infrastructure
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser_WhenUserIsValid()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var userRepository = new UserRepository(context);
                var user = new User { Name = "Test user", Email = "test@example.com" };

                await userRepository.AddAsync(user);

                var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
                Assert.NotNull(userInDb);
                Assert.Equal("Test user", userInDb.Name);
            }
        }

        [Fact]
        public async Task EmailExistsAsync_ShouldReturnTrue_WhenEmailExists()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                context.Users.Add(new User { Name = "Existing User", Email = "existing@example.com" });
                context.SaveChanges();

                var userRepository = new UserRepository(context);

                var exists = await userRepository.EmailExistsAsync("existing@example.com");

                Assert.True(exists);
            }
        }
    }
}
