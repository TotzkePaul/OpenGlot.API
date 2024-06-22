using System.Security.Claims;

namespace PolyglotAPI.Common
{
    public static class Extensions
    {
        public static Guid? ToGuid(this Claim? claim)
        {
            if (claim == null || string.IsNullOrEmpty(claim.Value))
                return null;

            return Guid.TryParse(claim.Value, out Guid result) ? result : null;
        }
    }
}
