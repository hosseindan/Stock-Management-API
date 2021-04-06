using System;
using System.Security.Claims;

namespace Carsales.StockManagement.Utility
{
    public static class IdentityExtensions
    {
        public static Guid GetDealerId(this ClaimsPrincipal identity)
        {
            Claim claim = identity?.FindFirst(CustomClaimTypes.DealerId);

            if (claim == null)
                return Guid.Empty;

            return Guid.Parse(claim.Value);
        }
    }
}
