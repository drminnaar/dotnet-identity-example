using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Api.Models;
using Identity.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("signups")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public sealed class SignupsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public SignupsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost(Name = nameof(Signup))]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(SignupDto signup)
        {
            if (await _userManager.Users.AnyAsync(u => u.Email == signup.Email.ToLower()))
            {
                ModelState.AddModelError("EmailAddress", "An account with that email address already exists");
                return new UnprocessableEntityObjectResult(new ValidationProblemDetails(ModelState));
            }

            var user = new AppUser
            {
                Email = signup.Email,
                UserName = signup.Email
            };

            var result = await _userManager.CreateAsync(user, signup.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);

                return new UnprocessableEntityObjectResult(new ValidationProblemDetails(ModelState));
            }

            return CreatedAtAction(
                actionName: nameof(UsersController.GetUser),
                controllerName: UsersController.ControllerName,
                routeValues: new { userId = user.Id },
                value: new UserDto
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserId = user.Id,
                    Username = user.UserName
                });
        }

        [HttpOptions(Name = nameof(GetSignupsOptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetSignupsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }
    }
}
