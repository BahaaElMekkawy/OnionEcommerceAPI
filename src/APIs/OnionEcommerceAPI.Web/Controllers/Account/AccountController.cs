using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Auth;
using OnionEcommerceAPI.Host.Controllers.Base;

namespace OnionEcommerceAPI.Web.Controllers.Account
{
    public class AccountController : ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var res = await _serviceManager.AuthService.LoginAsync(loginDto);
            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDtoDto)
        {
            var res = await _serviceManager.AuthService.RegisterAsync(registerDtoDto);
            return Ok(res);
        }

    }
}
