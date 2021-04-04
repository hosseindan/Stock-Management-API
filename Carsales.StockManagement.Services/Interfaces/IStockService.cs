using Carsales.StockManagement.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Services.Interfaces
{
    public interface IStockService
    {
        Task UpdateAvailableStock(Guid dealerId, UpdateStockRequest request);
        Task<GetStockResponse> GetAsync(Guid id);
        Task<List<GetStockResponse>> GetStocksForDealerAsync(Guid dealerId);
        Task<GetStockResponse> GetStocksForDealerAndCarAsync(Guid dealerId, Guid carId);
    }
}
