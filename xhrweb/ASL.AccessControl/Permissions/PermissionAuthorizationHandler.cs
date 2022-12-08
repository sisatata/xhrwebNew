using ASL.AccessControl.Identity;
using ASL.AccessControl.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASL.AccessControl.Permissions
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null) return;
            var userName = context.User.Claims.Where(x => x.Type == ClaimTypes.Name)
               .Select(x => x.Value).FirstOrDefault();
            if (userName == null) return;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return;
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).ToList();

            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.PERMISSION &&
                                                    x.Value == requirement.Permission).Select(x => x.Value);

                if (permissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }

            }
        }
    }
}
