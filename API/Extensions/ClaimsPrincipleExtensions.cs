using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user){
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static string GetEmail(this ClaimsPrincipal user){
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}