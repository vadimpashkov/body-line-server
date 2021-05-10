﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public sealed class UserRoleEntity : IdentityUserRole<int>
    {
        public IdentityRole<int> Role { get; set; }
    }
}
