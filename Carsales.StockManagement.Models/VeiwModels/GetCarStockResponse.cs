using System;

namespace Carsales.StockManagement.Models.VeiwModels
{
    public class GetCarStockResponse
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int AvailableStock { get; set; } = 0;
    }
}
