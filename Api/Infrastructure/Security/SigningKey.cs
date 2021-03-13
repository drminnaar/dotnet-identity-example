using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Infrastructure.Security
{
    public sealed class SigningKey
    {
        private readonly string _secret;

        public SigningKey(string secret)
        {
            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new ArgumentException($"'{nameof(secret)}' cannot be null or whitespace.", nameof(secret));
            }

            _secret = secret;
        }

        public SymmetricSecurityKey Value => new(Encoding.UTF8.GetBytes(_secret));
    }
}
