using System;

namespace Carsales.StockManagement.Models.VeiwModels
{
    public class CreateCarRequest : UpdateCarRequest
    {
        public Guid Id { get; set; }
    }
}
