using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public sealed class TokenCredentialsDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
