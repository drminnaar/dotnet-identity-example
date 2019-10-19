using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Models
{
    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        public virtual AppUser User { get; set; }
    }
}