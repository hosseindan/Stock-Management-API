namespace Carsales.StockManagement.Models.Contracts
{
    public class GetStockResponse
    {
        public string CarName { get; set; }
        public int AvailableStock { get; set; }
    }
}
