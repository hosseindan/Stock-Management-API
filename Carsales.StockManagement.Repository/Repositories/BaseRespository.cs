using System.Threading.Tasks;


namespace Carsales.StockManagement.Repository
{
    public class BaseRespository
    {
        protected readonly CarsalesDbContext _context;
        #region Constructor
        public BaseRespository(CarsalesDbContext context)
        {
            _context = context;
        }
        #endregion
        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
