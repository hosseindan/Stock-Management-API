using Carsales.StockManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Carsales.StockManagement.Repository
{
    public class StockTransactionRepository : BaseRespository, IStockTransactionRepository
    {

        #region Constructor
        public StockTransactionRepository(CarsalesDbContext context) :base(context)
        {
        }
        #endregion
        public void Delete(StockTransaction entity)
        {
            _context.StockTransactions.Remove(entity);
        }
        public async Task<StockTransaction> GetAsync(Guid id)
        {
            return await _context.StockTransactions.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<StockTransaction> InsertAsync(StockTransaction entity)
        {
            await _context.StockTransactions.AddAsync(entity);
            return entity;
        }
        public StockTransaction Update(StockTransaction entity)
        {
             _context.StockTransactions.Update(entity);
            return entity;
        }
    }
}
