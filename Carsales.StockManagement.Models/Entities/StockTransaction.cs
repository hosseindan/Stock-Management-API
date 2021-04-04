using System;
using System.Collections.Generic;
using System.Text;

namespace Carsales.StockManagement.Models.Entities
{
    public class StockTransaction:BaseEntity
    {
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public Guid DealerId { get; set; }
        public Dealer Dealer { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
    }
}
