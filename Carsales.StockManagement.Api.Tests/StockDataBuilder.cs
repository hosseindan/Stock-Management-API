using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Carsales.StockManagement.Api.Tests
{
    internal class StockDataBuilder
    {
        public readonly Guid _carOneId = Guid.NewGuid();
        public readonly Guid _carTwoId = Guid.NewGuid();
        public readonly Guid _dealreId = Guid.NewGuid();
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
                    Id = _carOneId,
                    Make = "BMW",
                    Model="X2",
                    Year=1999

                };

                var carTwo = new Car()
                {
                    Id = _carTwoId,
                    Make = "Toyota",
                    Model = "Kluger",
                    Year = 2020
                };

                context.AddRange(carOne, carTwo);

                var stockLevelOne = new Stock()
                {
                    AvailableStock=5,
                    CarId= _carOneId,
                    DealerId= Guid.NewGuid()
                };

                var stockLevelTwo = new Stock()
                {
                    AvailableStock = 15,
                    CarId = _carOneId,
                    DealerId = _dealreId
                };

                var stockLevelThree = new Stock()
                {
                    AvailableStock = 1,
                    CarId = _carTwoId,
                    DealerId = Guid.NewGuid()
                };
                context.AddRange(stockLevelOne, stockLevelTwo, stockLevelThree);

                context.SaveChanges();
            }
        }
    }
}
