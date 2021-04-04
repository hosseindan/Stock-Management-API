using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Carsales.StockManagement.Repository
{
    public static class RegisterServices
    {
        public static void RegisterDdServices(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
            //services.AddScoped<IDealerRepository, DealerRepository>();
        }
        public static void AddCarsalesDbContext(this IServiceCollection services , Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<CarsalesDbContext>(optionsAction);
        }
    }
}
