namespace Carsales.StockManagement.Models.Contracts
{
    public class GetStockResponse
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int AvailableStock { get; set; }
    }
}
