using System;

namespace Carsales.StockManagement.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid DealerId { get; set; }
    }
}
