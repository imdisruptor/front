using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Annotations;
using Backend.Models;
using Backend.Models.Entities;
using Backend.Services.Interfaces;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Backend.Controllers.Api
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly JwtIssuerOptions _jwtOptions;

        public TokenController(UserManager<ApplicationUser> userManager, IJwtService jwtService, IOptions<JwtIssuerOptions> jwtOptions, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _jwtOptions = jwtOptions.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> GetToken([FromBody]LoginViewModel model)
        {
            var identity = await GetClaimsIdentity(model.Email, model.Password);

            if (identity == null)
            {
                return BadRequest(new MessageModel { Message = "Не правильный email или пароль." });
            }

            var jwt = new TokenModel
            {
                AccessToken = await _jwtService.GenerateEncodedToken(identity),
            };

            return Ok(jwt);

        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user))
            {
                throw new ArgumentException("Вход заблокирован. Повторите попытку позже.");
            }

            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                if (_userManager.SupportsUserLockout && await _userManager.GetAccessFailedCountAsync(user) > 0)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                }

                return await Task.FromResult(_jwtService.GenerateClaimsIdentity(userName, user.Id, userRoles));
            }
            else
            {
                if (_userManager.SupportsUserLockout && await _userManager.GetLockoutEnabledAsync(user))
                {
                    await _userManager.AccessFailedAsync(user);
                }
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
