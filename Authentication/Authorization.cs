using Microsoft.AspNetCore.Authorization;
using PolyglotAPI.Data;
using PolyglotAPI.Data.Models;
using System.Security.Claims;

namespace PolyglotAPI.Authentication
{
    public class Authorization
    {
        public class RoleRequirement : IAuthorizationRequirement
        {
            public UserRole Role { get; }

            public RoleRequirement(UserRole role)
            {
                Role = role;
            }
        }

        public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
        {
            private readonly ApplicationDbContext _context;

            public RoleRequirementHandler(ApplicationDbContext context)
            {
                _context = context;
            }
            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
            {
                var subClaim = context.User.FindFirst(c => c.Type == "sub");
                if (subClaim == null)
                {
                    context.Fail();
                    return;
                }

                var userId = Guid.Parse(subClaim.Value);
                var user = await _context.UserProfiles.FindAsync(userId);

                if (user != null && user.UserRole == requirement.Role)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
        }
    }
}
