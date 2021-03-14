using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public virtual ICollection<AppUserClaim> Claims { get; set; } = null!;
        public virtual ICollection<AppUserLogin> Logins { get; set; } = null!;
        public virtual ICollection<AppUserToken> Tokens { get; set; } = null!;
        public virtual ICollection<AppUserRole> UserRoles { get; set; } = null!;
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
