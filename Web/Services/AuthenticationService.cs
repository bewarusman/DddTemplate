using Application.Common.Interfaces;
using System.Security.Claims;

namespace Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var Name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            return Name;
        }



        public string GetUserId()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return id;
        }
        public string GetUserIp()
        {
            var ip = _httpContextAccessor.HttpContext
                ?.Connection
                ?.RemoteIpAddress
                ?.ToString();
            return ip;
        }
    }
}
