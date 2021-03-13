using System;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Models;
using Identity.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("users")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        internal static string ControllerName =>
            nameof(UsersController).Replace("Controller", string.Empty);

        [HttpGet(Name = nameof(CurrentUser))]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (user is null)
                return Unauthorized();

            return Ok(new UserDto
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                UserId = user.Id,
                Username = user.UserName
            });
        }

        [HttpGet("{userId:Guid}", Name = nameof(GetUser))]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                return NotFound();

            return Ok(new UserDto
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                UserId = user.Id,
                Username = user.UserName
            });
        }

        [HttpOptions(Name = nameof(GetUsersOptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }
    }
}
