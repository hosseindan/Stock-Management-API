using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Carsales.StockManagement.Models.VeiwModels
{
    [DisplayName("GetCarResponse")]
    public class GetCarResponse
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
