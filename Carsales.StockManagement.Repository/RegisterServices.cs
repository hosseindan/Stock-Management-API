using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Carsales.StockManagement.Repository
{
    public static class RegisterServices
    {
        public static void RegisterDdServices(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
        }
        public static void AddCarsalesDbContext(this IServiceCollection services , Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<CarsalesDbContext>(optionsAction);
        }
    }
}
