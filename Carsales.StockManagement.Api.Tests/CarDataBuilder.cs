using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Carsales.StockManagement.Api.Tests
{
    internal class CarDataBuilder
    {
        private static readonly Guid _id = Guid.NewGuid();
        protected DbContextOptions<CarsalesDbContext> ContextOptions { get; }
        public CarDataBuilder(DbContextOptions<CarsalesDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }
        public static List<Car> GetCars()
        {
            return new List<Car>()
                {
                    new  Car()
                        {
                            Id = _id,
                            Make = "BMW",
                            Model = "X3"
                        },
                     new Car()
                        {
                            Id = Guid.NewGuid(),
                            Make = "Toyota",
                            Model = "Kluger"
                        }
                };
        }
        public static Car GetCar()
        {
            return new Car()
            {
                Id = _id,
                Make = "BMW",
                Model = "X3"
            };
        }
        public static CreateCarRequest GetCreateCarRequest()
        {
            return new CreateCarRequest()
            {
                Id = _id,
                Make = "BMW",
                Model = "X3"
            };
        }
        public static UpdateCarRequest GetUpdateCarRequest()
        {
            return new UpdateCarRequest()
            {
                Make = "BMW",
                Model = "X4"
            };
        }
        public void Seed()
        {
            using (var context = new CarsalesDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Car()
                {
                    Id = _id,
                    Make = "BMW",
                    Model="X2"
                };

                var two = new Car()
                {
                    Id = Guid.NewGuid(),
                    Make = "Benz"
                };

                context.AddRange(one, two);

                context.SaveChanges();
            }
        }
    }
}
