using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Api.Configuration;
using Api.Infrastructure.Security;
using Api.Models;
using Identity.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [Route("tokens")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public sealed class TokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly JwtGenerator _jwtGenerator;

        public TokensController(
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IOptions<JwtSettings> jwtSettings,
            JwtGenerator jwtGenerator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
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

            var signinResult = await _signInManager.PasswordSignInAsync(
                user: user,
                password: credentials.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!signinResult.Succeeded)
                return Unauthorized();

            return Ok(new TokenDto
            {
                Token = _jwtGenerator.GenerateToken(
                    user: user,
                    tokenExpiryTime: _jwtSettings.Value.ExpiryTimeSpanInSeconds,
                    securityKey: SigningKeyFactory.CreateSigningKey(_configuration))
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
