using Carsales.StockManagement.Api.Controllers;
using Carsales.StockManagement.Models.Validators;
using Carsales.StockManagement.Models.VeiwModels;
using Carsales.StockManagement.Repository;
using Carsales.StockManagement.Services;
using Carsales.StockManagement.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace Carsales.StockManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IStockService, StockService>()
                .AddScoped<CarsController>()
                .AddScoped<StocksController>()
                .AddScoped<IValidator<UpdateCarRequest>, UpdateCarValidator>()
                .AddScoped<IValidator<CreateCarRequest>, CreateCarValidator>();
            services
                .Configure<DbSettings>(Configuration)
                .AddOptions<DbSettings>();
            services.RegisterDdServices();
            services.AddCarsalesDbContext(options => options.UseInMemoryDatabase("Carsales"));
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
                    {
                        //c.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>()?.SingleOrDefault()?.DisplayName);
                        // Set the comments path for the Swagger JSON and UI.
                        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        c.IncludeXmlComments(xmlPath);
                        c.EnableAnnotations();
                    }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock management API");
                c.RoutePrefix = "docs";
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
