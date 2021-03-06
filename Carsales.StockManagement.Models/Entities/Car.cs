using System.Collections.Generic;

namespace Carsales.StockManagement.Models.Entities
{
    public class Car : BaseEntity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
