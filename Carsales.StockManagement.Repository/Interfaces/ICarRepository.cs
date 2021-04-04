using Carsales.StockManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Carsales.StockManagement.Repository
{
    public interface ICarRepository  : IBaseRepository<Car>
    {
        Task<List<Car>> GetAsync(Expression<Func<Car, bool>> searchCriteria);
        Task<List<Car>> GetCarsAndStockLevelsAsync(Guid dealerId);
        Task<bool> IsDeletable(Guid id);
    }
}

