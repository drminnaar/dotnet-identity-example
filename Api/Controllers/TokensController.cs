using System;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Infrastructure.Security;
using Api.Models;
using Identity.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("tokens")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public sealed class TokensController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public TokensController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost(Name = "GetToken")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetToken([FromBody] TokenCredentialsDto credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);

            if (user is null)
                return Unauthorized();

            var signinResult = await _signInManager.CheckPasswordSignInAsync(
                user: user,
                password: credentials.Password,
                lockoutOnFailure: false);

            if (!signinResult.Succeeded)
                return Unauthorized();

            await _tokenService.SetRefreshToken(user);

            return Ok(new TokenDto
            {
                Token = _tokenService.GenerateToken(user)
            });
        }

        [HttpPost("refresh-token", Name = "RefreshToken")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (user is null)
                return Unauthorized();

            var refreshToken = await _tokenService.GenerateRefreshToken(user);

            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            return Ok(new TokenDto
            {
                Token = refreshToken
            });
        }

        [HttpOptions(Name = nameof(GetTokensOptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetTokensOptions()
        {
            Response.Headers.Add("Allow", "POST,OPTIONS");
            return Ok();
        }
    }
}
