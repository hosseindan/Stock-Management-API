using Carsales.StockManagement.Api.Controllers;
using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using Carsales.StockManagement.Utility;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Carsales.StockManagement.Api.Tests
{

    public class StockControllerTests
    {
        protected DbContextOptions<CarsalesDbContext> ContextOptions { get; }
        public StockControllerTests()
        {
            var builder = new DbContextOptionsBuilder<CarsalesDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            ContextOptions = builder.Options;
        }
        [Fact]
        public async Task Can_update_stock_Level()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var stockController = CreateService();
                var dataBuilder = new StockDataBuilder(ContextOptions);
                dataBuilder.Seed();
                //set user claims
                SetUserClaims(stockController, dataBuilder.DealerId);
                var updateRequest = new UpdateStockRequest()
                {
                    CarId = dataBuilder.CarOneId,
                    Quantity = 5
                };
                //Act
                var result = await stockController.Put(updateRequest) as OkResult;

                //Assert
                result.StatusCode.Should().Be(200);
                var actualStock = context.Stocks.FirstOrDefault(c => c.DealerId == dataBuilder.DealerId && c.CarId == dataBuilder.CarOneId);
                Assert.Equal(updateRequest.Quantity, actualStock.AvailableStock);
            }
        }
        [Fact]
        public async Task Can_update_stock_level_for_first_time()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var stockController = CreateService();
                var dataBuilder = new StockDataBuilder(ContextOptions);
                dataBuilder.Seed();
                //set user claims
                SetUserClaims(stockController, dataBuilder.DealerId);

                var updateRequest = new UpdateStockRequest()
                {
                    CarId = dataBuilder.CarTwoId,
                    Quantity = 5
                };

                //Act
                var result = await stockController.Put(updateRequest) as OkResult;

                //Assert
                result.StatusCode.Should().Be(200);
                var actualStock = context.Stocks.FirstOrDefault(c => c.DealerId == dataBuilder.DealerId && c.CarId == dataBuilder.CarTwoId);
                Assert.Equal(updateRequest.Quantity, actualStock.AvailableStock);
            }
        }
        [Fact]
        public async Task Given_dealer_can_not_access_another_dealer_stock_levels()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var stockController = CreateService();
                var databuilder = new StockDataBuilder(ContextOptions);
                databuilder.Seed();
                //set user claims
                SetUserClaims(stockController, databuilder.DealerId);

                //Act
                var actionResult = await stockController.GetCarsAndStockLevels();
                var result = actionResult.Result as OkObjectResult;

                //Assert
                result.StatusCode.Should().Be(200);
                result.Value.As<List<GetCarStockResponse>>().Count.Should().Be(1);
                result.Value.As<List<GetCarStockResponse>>().First(f => f.CarId == databuilder.CarOneId).AvailableStock.Should().Be(15);

            }
        }
        private StocksController CreateService() => new DependencyResolver(
           sc =>
           {
               sc.AddSingleton<DbContextOptions<CarsalesDbContext>>(ContextOptions);
           })
            .GetService<StocksController>();
        private void SetUserClaims(StocksController stockController, Guid dealerId)
        {
            //set user claims
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, "hossein@hossein.com"),
                                        new Claim("DealerId", dealerId.ToString())
                                     }, "TestAuthentication"));

            stockController.ControllerContext = new ControllerContext();
            stockController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }
    }
}
