using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public sealed class SignupDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        public string Password { get; init; } = string.Empty;
    }
}
