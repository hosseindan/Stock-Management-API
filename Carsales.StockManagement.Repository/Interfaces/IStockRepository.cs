using Carsales.StockManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Repository
{
    public interface IStockRepository
    {
        Task<Stock> GetAsync(Guid id);
        Task<List<Stock>> GetStocksForDealerAsync(Guid dealerId);
        Task<Stock> GetStocksForDealerAndCarAsync(Guid dealerId, Guid carId);
        void Update(Stock stock);
        Task InsertAsync(Stock stock);
        Task SaveAsync();
    }
}

