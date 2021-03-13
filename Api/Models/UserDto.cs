using System;

namespace Api.Models
{
    public sealed class UserDto
    {
        public Guid UserId { get; init; } = Guid.Empty;
        public string Username { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
    }
}
