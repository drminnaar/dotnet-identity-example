using Api.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Infrastructure.Security
{
    public static class SigningKeyFactory
    {
        public static SymmetricSecurityKey CreateSigningKey(IConfiguration configuration)
        {
            var secret = JwtSecuritySettings
                .FromConfiguration(configuration)
                .Secret;

            return new SigningKey(secret).Value;
        }
    }
}
