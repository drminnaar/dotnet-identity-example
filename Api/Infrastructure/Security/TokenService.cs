using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Api.Infrastructure.Configuration;
using Identity.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Infrastructure.Security
{
    public class TokenService
    {
        private const string COOKIE_NAME = "refresh-token";
        private readonly JwtSettings _jwtSettings;
        private readonly JwtSecuritySettings _jwtSecuritySettings;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenService(
            IOptions<JwtSettings> jwtSettings,
            IOptions<JwtSecuritySettings> jwtSecuritySettings,
            UserManager<AppUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _jwtSettings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _jwtSecuritySettings = jwtSecuritySettings?.Value ?? throw new ArgumentNullException(nameof(jwtSecuritySettings));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public string GenerateToken(AppUser user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpiryTimeSpanInSeconds),
                SigningCredentials = new(
                    new SigningKey(_jwtSecuritySettings.Secret).Value,
                    SecurityAlgorithms.HmacSha512)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string?> GenerateRefreshToken(AppUser user)
        {
            var refreshToken = _contextAccessor
                ?.HttpContext
                ?.Request
                ?.Cookies[COOKIE_NAME];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return null;

            var usernameFromContext = _contextAccessor
                ?.HttpContext
                ?.User
                ?.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(usernameFromContext))
                return null;

            var userFromStore = await _userManager
                .Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.UserName == usernameFromContext);

            if (userFromStore == null)
                return null;

            var previousToken = user
                .RefreshTokens
                .SingleOrDefault(rt => rt.Value == refreshToken);

            if (previousToken is not null && !previousToken.IsActive)
                return null;

            return GenerateToken(user);
        }

        public async Task SetRefreshToken(AppUser user)
        {
            var refreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiresAt
            };

            _contextAccessor
                .HttpContext
                ?.Response
                ?.Cookies
                ?.Append(COOKIE_NAME, refreshToken.Value, cookieOptions);
        }

        private RefreshToken CreateRefreshToken()
        {
            var data = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);
            return new RefreshToken
            {
                Value = Convert.ToBase64String(data)
            };
        }
    }
}
