using Carsales.StockManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Carsales.StockManagement.Repository
{
    public class StockRepository : BaseRespository, IStockRepository
    {
        #region Constructor
        public StockRepository(CarsalesDbContext context) : base(context)
        {
        }
        #endregion
        public async Task<Stock> GetAsync(Guid id)
        {
            return await _context.Stocks.SingleOrDefaultAsync(s=>s.Id== id);
        }
        public async Task<List<Stock>> GetStocksForDealerAsync(Guid dealerId)
        {
            return await _context.Stocks.Include(x=>x.Car).Where(s=>s.DealerId==dealerId).ToListAsync();
        }
        public async Task<Stock> GetStocksForDealerAndCarAsync(Guid dealerId, Guid carId)
        {
            return await _context.Stocks.Where(s => s.DealerId == dealerId && s.CarId==carId).SingleOrDefaultAsync();
        }
        public async Task InsertAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
        }
        public void Update(Stock stock)
        {
             _context.Stocks.Update(stock);
        }
    }
}
