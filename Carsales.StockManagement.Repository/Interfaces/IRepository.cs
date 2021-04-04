using System;
using System.Text;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        void Delete(T entity);
        Task<T> InsertAsync(T entity);
        T Update(T entity);
        Task SaveAsync();
    }

}
