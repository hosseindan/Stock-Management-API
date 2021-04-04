using Carsales.StockManagement.Models;
using Carsales.StockManagement.Models.VeiwModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Services
{
    public interface ICarService
    {
        Task<GetCarResponse> GetAsync(Guid id);
        Task<List<GetCarResponse>> GetAsync(CarSearchCriteria searchCriteria);
        Task<List<GetCarStockResponse>> GetCarsAndStockLevelsAsync(Guid dealerId);
        Task DeleteAsync(Guid id);
        Task InsertAsync(CreateCarRequest request);
        Task UpdateAsync(Guid id, UpdateCarRequest request);
    }
}
