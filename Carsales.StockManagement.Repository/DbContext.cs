using Carsales.StockManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Carsales.StockManagement.Repository
{
    public class CarsalesDbContext : DbContext
    {
        public CarsalesDbContext(DbContextOptions<CarsalesDbContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
