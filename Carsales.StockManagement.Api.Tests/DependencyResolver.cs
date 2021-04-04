using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Carsales.StockManagement.Api.Tests
{
    public class DependencyResolver
    {
        private readonly IHost _host;
        public DependencyResolver(Action<IServiceCollection> buildTestServices)
        {
            _host= Host.CreateDefaultBuilder(new string[0])
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(buildTestServices)
                .Build();
        }
        public T GetService<T>() where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }
    }

}
