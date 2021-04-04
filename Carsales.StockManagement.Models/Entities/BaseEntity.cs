using System;

namespace Carsales.StockManagement.Models.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; } = null;
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; } = null;
    }
}
