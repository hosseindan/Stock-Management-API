﻿using Carsales.StockManagement.Api.Controllers;
using Carsales.StockManagement.Models;
using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using Carsales.StockManagement.Services;
using Carsales.StockManagement.Services.Interfaces;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Carsales.StockManagement.Api.Tests
{
    public class CarControllerTests
    {
        protected DbContextOptions<CarsalesDbContext> ContextOptions { get; }
        public CarControllerTests()
        {
            var builder = new DbContextOptionsBuilder<CarsalesDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            ContextOptions = builder.Options;
        }

        [Fact]
        public async Task Can_add_car()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var carController = CreateService();

                //Act
                var result = await carController.Post(CarDataBuilder.GetCreateCarRequest()) as OkResult;

                //Assert
                result.StatusCode.Should().Be(200);
                var actualCar = context.Cars.FirstOrDefault(c => c.Id == CarDataBuilder.GetCreateCarRequest().Id);
                Assert.Equal(CarDataBuilder.GetCreateCarRequest().Make, actualCar.Make);
            }
        }
        [Fact]
        public async Task Can_delete_car()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var carController = CreateService();
                context.Add<Car>(CarDataBuilder.GetCar());
                await context.SaveChangesAsync();

                //Act
                var result = await carController.Delete(CarDataBuilder.GetCar().Id) as OkResult;

                //Assert
                result.StatusCode.Should().Be(200);
                var actualCar = context.Cars.FirstOrDefault(c => c.Id == CarDataBuilder.GetCreateCarRequest().Id);
                Assert.Null(actualCar);
            }
        }
        [Fact]
        public async Task Given_invalid_car_should_return_not_founnd()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var carController = CreateService();
                context.Add<Car>(CarDataBuilder.GetCar());
                await context.SaveChangesAsync();

                //Act
                var actionResult = await carController.Delete(CarDataBuilder.GetCar().Id) ;

                //Assert
                actionResult.Should().BeOfType(typeof(NotFoundResult));
            }
        }
        [Fact]
        public async Task Can_search_car()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var carController = CreateService();
                await context.AddRangeAsync(CarDataBuilder.GetCars());
                await context.SaveChangesAsync();
                var searrchRequest = new CarSearchCriteria()
                {
                    Make = CarDataBuilder.GetCars().First().Make
                };

                //Act
                var actionResult = await carController.Search( searrchRequest);
                var result = actionResult.Result as OkObjectResult;

                //Assert
                result.StatusCode.Should().Be(200);
                result.Value.As<List<GetCarResponse>>().Count.Should().Be(1);
                result.Value.As<List<GetCarResponse>>().First().Id.Should().Be(CarDataBuilder.GetCars().First().Id);
            }
        }
        [Fact]
        public async Task Given_dealer_not_access_another_dealer_stock_levela()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                //Arrange
                var carController = CreateService();
                var databuilder = new StockDataBuilder(ContextOptions);
                databuilder.Seed();

                //Act
                var actionResult = await carController.GetCarsAndStockLevels(databuilder._dealreId);
                var result = actionResult.Result as OkObjectResult;

                //Assert
                result.StatusCode.Should().Be(200);
                result.Value.As<List<GetCarStockResponse>>().Count.Should().Be(2);
                result.Value.As<List<GetCarStockResponse>>().First(f => f.Id == databuilder._carOneId).AvailableStock.Should().Be(15);
                result.Value.As<List<GetCarStockResponse>>().First(f => f.Id == databuilder._carTwoId).AvailableStock.Should().Be(0);

            }
        }
        private CarsController CreateService() => new DependencyResolver(
        sc =>
        {
            sc.AddSingleton<DbContextOptions<CarsalesDbContext>>(ContextOptions);
        })
        .GetService<CarsController>();

    }
}
