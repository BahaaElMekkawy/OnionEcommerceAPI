using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts.Auth;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Auth;
using OnionEcommerceAPI.Core.Application.Common.Exception;
using OnionEcommerceAPI.Core.Domain.Entities.Identity;

namespace OnionEcommerceAPI.Core.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new UnauthorizedException("Invalid Login");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (result.IsNotAllowed)
                throw new UnauthorizedException("Please Confirm Your Account Before Login");

            if (result.IsLockedOut)
                throw new UnauthorizedException("Account is Locked");

            if (!result.Succeeded)
                throw new UnauthorizedException("Invalid Login");

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateToken(user)
            };

            return response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new ValidationException()
                {
                    Errors = result.Errors.Select(E => E.Description)
                };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateToken(user)
            };

            return response;
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = new List<Claim>();
            foreach (var role in userRoles)
                roles.Add(new Claim(ClaimTypes.Role, role));

            var claims = new List<Claim>() {
                new Claim (ClaimTypes.PrimarySid, user.Id),
                new Claim (ClaimTypes.Email, user.Email!),
                new Claim (ClaimTypes.GivenName, user.DisplayName)
            }
            .Union(roles).Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:DurationInMin"]!)),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObject);
        }
    }
}
