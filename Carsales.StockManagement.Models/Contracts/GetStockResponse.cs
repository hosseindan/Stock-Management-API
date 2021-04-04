using Carsales.StockManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carsales.StockManagement.Models.Contracts
{
    public class GetStockResponse
    {
        public string CarName { get; set; }
        public int AvailableStock { get; set; }
    }
}
