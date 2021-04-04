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
        Task<List<GetCarStockResponse>> GetAsync(Guid dealerId, CarSearchCriteria searchCriteria);
        Task<List<GetCarStockResponse>> GetCarsAndStockLevelsAsync(Guid dealerId);
        Task DeleteAsync(Guid id);
        Task InsertAsync(CreateCarRequest model);
        Task UpdateAsync(Guid id, UpdateCarRequest model);
    }
}
