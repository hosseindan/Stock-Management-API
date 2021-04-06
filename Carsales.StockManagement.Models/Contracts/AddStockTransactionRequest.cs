using Carsales.StockManagement.Models.Entities;
using System;

namespace Carsales.StockManagement.Models.Contracts
{
    public class AddStockTransactionRequest
    {
        public Guid CarId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
    }
}
