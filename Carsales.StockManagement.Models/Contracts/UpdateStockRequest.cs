using Carsales.StockManagement.Models.Entities;
using Newtonsoft.Json.Converters;
using System;
using System.Text.Json.Serialization;

namespace Carsales.StockManagement.Models.Contracts
{
    public class UpdateStockRequest
    {
        public Guid CarId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
    }
}
