using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Models
{
    public class AppRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual AppRole Role { get; set; }
    }
}