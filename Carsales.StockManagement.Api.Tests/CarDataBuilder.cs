using Carsales.StockManagement.Models.Entities;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Carsales.StockManagement.Api.Tests
{
    internal class CarDataBuilder
    {
        private static readonly Guid ValidCarId = Guid.NewGuid();
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
                            Id = ValidCarId,
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
                Id = ValidCarId,
                Make = "BMW",
                Model = "X3"
            };
        }
        public static CreateCarRequest GetCreateCarRequest()
        {
            return new CreateCarRequest()
            {
                Id = ValidCarId,
                Make = "BMW",
                Model = "X3",
                Year = 2021
            };
        }
    }
}
