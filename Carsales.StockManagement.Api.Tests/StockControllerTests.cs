using Carsales.StockManagement.Api.Controllers;
using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
                var updateRequest = new UpdateStockRequest()
                {
                    CarId = dataBuilder.CarOneId,
                    Quantity = 5,
                    TransactionType=Models.Entities.TransactionType.Increase
                };
                //Act
                var result = await stockController.Put(dataBuilder.DealerId, updateRequest) as OkResult;

                //Assert
                result.StatusCode.Should().Be(200);
                var actualStock = context.Stocks.FirstOrDefault(c => c.DealerId == dataBuilder.DealerId && c.CarId==dataBuilder.CarOneId);
                Assert.Equal(20, actualStock.AvailableStock);
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
                var updateRequest = new UpdateStockRequest()
                {
                    CarId = dataBuilder.CarTwoId,
                    Quantity = 5,
                    TransactionType = Models.Entities.TransactionType.Increase
                };
                //Act
                var result = await stockController.Put(dataBuilder.DealerId, updateRequest) as OkResult;

                //Assert
                result.StatusCode.Should().Be(200);
                var actualStock = context.Stocks.FirstOrDefault(c => c.DealerId == dataBuilder.DealerId && c.CarId == dataBuilder.CarTwoId);
                Assert.Equal(updateRequest.Quantity, actualStock.AvailableStock);
            }
        }

        private StocksController CreateService() => new DependencyResolver(
           sc =>
           {
               sc.AddSingleton<DbContextOptions<CarsalesDbContext>>(ContextOptions);
           })
       .GetService<StocksController>();
    }
}
