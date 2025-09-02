using System.Security.Claims;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;

namespace OnionEcommerceAPI.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public string? UserId { get; } 

        public CurrentUserService(IHttpContextAccessor? httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            UserId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
    }
}
