using System;

namespace Carsales.StockManagement.Models.Entities
{
    public class Stock: BaseEntity
    {
        public int AvailableStock { get; set; }
        public Guid DealerId { get; set; }
        public Dealer Dealer { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
