using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Carsales.StockManagement.Models.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionType
    {
        Increase=0,//Receive
        Decrease =1///TransferOut
    }
}
