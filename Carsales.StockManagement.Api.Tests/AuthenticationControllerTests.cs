using Carsales.StockManagement.Api.Controllers;
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
        private AuthenticationController CreateService() => new DependencyResolver(
        sc =>
        {
            sc.AddSingleton<DbContextOptions<CarsalesDbContext>>(ContextOptions);
        })
        .GetService<AuthenticationController>();
    }
}
