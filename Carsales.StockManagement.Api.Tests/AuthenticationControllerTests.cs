using Carsales.StockManagement.Api.Controllers;
using Carsales.StockManagement.Models;
using Carsales.StockManagement.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Carsales.StockManagement.Api.Tests
{
    public class AuthenticationControllerTests
    {
        protected DbContextOptions<CarsalesDbContext> ContextOptions { get; }
        public AuthenticationControllerTests()
        {
            var builder = new DbContextOptionsBuilder<CarsalesDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            ContextOptions = builder.Options;
        }
        [Fact]
        public async Task Can_get_token()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var authenticationController = CreateService();

                //Act
                var result = await authenticationController.Get() as OkObjectResult;

                //Assert
                result.Value.Should().NotBeNull();
                Assert.StartsWith("Bearer",result.Value.ToString());
            }
        }
        [Fact]
        public async Task Can_login()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var authenticationController = CreateService();
                AuthenticationModel authenticationModel = new AuthenticationModel()
                {
                    Username="dealer1",
                    Password="123"
                };
                //Act
                var result = await authenticationController.Login(authenticationModel) as OkObjectResult;

                //Assert
                result.Value.Should().NotBeNull();
                Assert.StartsWith("Bearer", result.Value.ToString());
            }
        }
        [Fact]
        public async Task Given_invalid_user_password_should_return_not_found()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var authenticationController = CreateService();
                AuthenticationModel authenticationModel = new AuthenticationModel()
                {
                    Username = "dealer1",
                    Password = "123456"
                };
                //Act
                var actionResult = await authenticationController.Login(authenticationModel) ;

                //Assert
                actionResult.Should().BeOfType(typeof(NotFoundResult));
            }
        }
        [Fact]
        public async Task Given_empty_username_or_password_should_return_not_found()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var authenticationController = CreateService();
                AuthenticationModel authenticationModel = new AuthenticationModel()
                {
                    Username = "dealer1",
                    Password = null
                };
                //Act
                var actionResult = await authenticationController.Login(authenticationModel);

                //Assert
                actionResult.Should().BeOfType(typeof(BadRequestResult));
            }
        }
        private AuthenticationController CreateService() => new DependencyResolver(
        sc =>
        {
            sc.AddSingleton<DbContextOptions<CarsalesDbContext>>(ContextOptions);
        })
        .GetService<AuthenticationController>();
    }
}
