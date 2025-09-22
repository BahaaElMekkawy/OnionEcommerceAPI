using OnionEcommerceAPI.Core.Application.Abstractions.Models.Auth;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);
    }
}
