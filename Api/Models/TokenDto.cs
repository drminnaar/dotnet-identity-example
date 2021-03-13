using System.Runtime.Serialization;

namespace Api.Models
{
    [DataContract(Name = "Token")]
    public sealed class TokenDto
    {
        [DataMember(IsRequired = true)]
        public string Token { get; init; } = string.Empty;
    }
}
