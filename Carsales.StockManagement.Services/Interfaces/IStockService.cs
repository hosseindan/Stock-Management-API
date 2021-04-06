using Carsales.StockManagement.Models.Contracts;
using Carsales.StockManagement.Models.VeiwModels;
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
        Task<List<GetCarStockResponse>> GetStocksForDealerAsync(Guid dealerId);
        Task<GetCarStockResponse> GetStocksForDealerAndCarAsync(Guid dealerId, Guid carId);
    }
}
