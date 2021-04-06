namespace Carsales.StockManagement.Models
{
    public class AuthenticationSettings
    {
        public string JwtSecretKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
    }
}
