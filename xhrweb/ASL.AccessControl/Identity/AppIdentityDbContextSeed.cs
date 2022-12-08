using ASL.AccessControl.Services.Security;
using ASL.AccessControl.Utility;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASL.AccessControl.Identity
{
    public class AppIdentityDbContextSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionProvider _permissionProvider;

        public AppIdentityDbContextSeed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IPermissionProvider permissionProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionProvider = permissionProvider;
        }

        public async Task SeedDefaultUserAsync()
        {
            await AddRole();
            
            var defaultUser = new ApplicationUser { UserName = "demouser@aplectrum.com", Email = "demouser@aplectrum.com" };
            await _userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
        }

        public async Task SeedAdminUserAsync(string adminUserName, string password)
        {
            await AddRole();

            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName, EmailConfirmed = true };
            await _userManager.CreateAsync(adminUser, password);
            //var createdUser = await _userManager.FindByEmailAsync(adminUserName);
            await _userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATORS);
        }

        private async Task AddRole()
        {
            var adminRole = await _roleManager.FindByNameAsync(AuthorizationConstants.Roles.ADMINISTRATORS);
            if(adminRole == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATORS));

                adminRole = await _roleManager.FindByNameAsync(AuthorizationConstants.Roles.ADMINISTRATORS);

                await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.PERMISSION, DefaultPermissionProvider.CreateUser.SystemName));
                await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.PERMISSION, DefaultPermissionProvider.EditUser.SystemName));
                await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.PERMISSION, DefaultPermissionProvider.DeleteUser.SystemName));
                await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.PERMISSION, DefaultPermissionProvider.ViewUser.SystemName));
            }
            else
            {
                // append permission to administrator role
                var existingPermissions = await _roleManager.GetClaimsAsync(adminRole);
                var permissionsName = existingPermissions.Select(c => c.Value).ToList();
                var newPermission = _permissionProvider.GetPermissions();

                newPermission = newPermission.Where(x => !permissionsName.Contains(x.SystemName)).ToList();
                if(newPermission != null)
                {
                    foreach (var item in newPermission)
                    {
                        await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.PERMISSION, item.SystemName));
                    }
                }
            }
        }
    }
}
