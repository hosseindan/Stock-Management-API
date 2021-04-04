using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Repository;
using Microsoft.EntityFrameworkCore;
using System;


namespace Carsales.StockManagement.Api.Tests
{
    internal class StockDataBuilder
    {
        public readonly Guid CarOneId = Guid.NewGuid();
        public readonly Guid CarTwoId = Guid.NewGuid();
        public readonly Guid DealerId = Guid.NewGuid();
        protected DbContextOptions<CarsalesDbContext> ContextOptions { get; }
        public StockDataBuilder(DbContextOptions<CarsalesDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }
        public void Seed()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var carOne = new Car()
                {
                    Id = CarOneId,
                    Make = "BMW",
                    Model="X2",
                    Year=1999

                };

                var carTwo = new Car()
                {
                    Id = CarTwoId,
                    Make = "Toyota",
                    Model = "Kluger",
                    Year = 2020
                };

                context.AddRange(carOne, carTwo);

                var stockLevelOne = new Stock()
                {
                    AvailableStock=5,
                    CarId= CarOneId,
                    DealerId= Guid.NewGuid()
                };

                var stockLevelTwo = new Stock()
                {
                    AvailableStock = 15,
                    CarId = CarOneId,
                    DealerId = DealerId
                };

                var stockLevelThree = new Stock()
                {
                    AvailableStock = 1,
                    CarId = CarTwoId,
                    DealerId = Guid.NewGuid()
                };
                context.AddRange(stockLevelOne, stockLevelTwo, stockLevelThree);

                context.SaveChanges();
            }
        }
    }
}
