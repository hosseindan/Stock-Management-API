using System.Collections.Generic;

namespace Carsales.StockManagement.Models.Entities
{
    public class Dealer :BaseEntity
    {
        public string Name { get; set; }
        public List<Stock> Stocks { get; set; } = new List<Stock>();
        public List<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();

    }
}
