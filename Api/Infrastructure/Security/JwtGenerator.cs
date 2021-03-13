using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Infrastructure.Security
{
    public sealed class JwtGenerator
    {
        public string GenerateToken(AppUser user, TimeSpan tokenExpiryTime, SecurityKey securityKey)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.Add(tokenExpiryTime),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
