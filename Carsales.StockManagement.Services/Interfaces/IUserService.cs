using Carsales.StockManagement.Models;

namespace Carsales.StockManagement.Services
{
    public interface IUserService
    {
        string GetToken(AuthenticationModel authenticationModel);
    }
}