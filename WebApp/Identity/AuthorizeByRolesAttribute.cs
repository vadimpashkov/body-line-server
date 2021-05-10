using System.Linq;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Schedule.Identity
{
    public class AuthorizeByRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeByRolesAttribute(params UserRole[] roles)
        {
            Roles = string.Join(", ", roles.Select(r => r.ToString()));
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
