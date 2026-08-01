using System.Security.Claims;

namespace ToDo.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)
             ?? throw new UnauthorizedAccessException("İstifadəçi tanınmadı.");

            return int.Parse(userIdClaim.Value);
        }
    }
}
