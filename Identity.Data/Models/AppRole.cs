using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }
}