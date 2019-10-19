using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Models
{
    public class AppUserToken : IdentityUserToken<Guid>
    {
        public virtual AppUser User { get; set; }
    }
}