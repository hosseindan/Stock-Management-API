using Carsales.StockManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Carsales.StockManagement.Repository
{
    public class CarRepository : BaseRespository, ICarRepository
    {

        #region Constructor
        public CarRepository(CarsalesDbContext context) : base(context)
        {
        }
        #endregion
        public void Delete(Car entity)
        {
            _context.Cars.Remove(entity);
        }
        public async Task<Car> GetAsync(Guid id)
        {
            return await _context.Cars.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Car>> GetAsync(Expression<Func<Car, bool>> searchCriteria)
        {
            return await _context.Cars.Where(searchCriteria).ToListAsync();
        }
        public async Task<List<Car>> GetCarsAndStockLevelsAsync(Guid dealerId)
        {
            return await _context.Cars.Include(x => x.Stocks.Where(s => s.DealerId == dealerId)).ToListAsync();
        }
        public async Task<bool> IsDeletable(Guid id)
        {
            return !await _context.Stocks.AnyAsync(s => s.CarId == id);
        }
        public async Task<Car> InsertAsync(Car entity)
        {
            await _context.Cars.AddAsync(entity);
            return entity;
        }
        public Car Update(Car entity)
        {
            _context.Cars.Update(entity);
            return entity;
        }
    }
}
