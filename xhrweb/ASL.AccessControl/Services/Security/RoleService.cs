using ASL.AccessControl.Identity;
using ASL.AccessControl.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASL.AccessControl.Services.Security
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
      

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> AssignRolePermission(string[] roleIds, string[] permissions)
        {
            try
            {
                foreach (var roleId in roleIds)
                {
                    var role = await _roleManager.FindByIdAsync(roleId);
                    if (role == null) continue;

                    // append permission to administrator role
                    var existingPermissions = await _roleManager.GetClaimsAsync(role);
                    var permissionsName = existingPermissions.Select(c => c.Value).ToList();

                    var newPermission = permissions.Where(x => !permissionsName.Contains(x)).ToList();

                    if (newPermission != null)
                    {
                        foreach (var item in newPermission)
                        {
                            await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.PERMISSION, item));
                        }
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateRole(string roleName)
        {
            try
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = _roleManager.Roles;
            return await roles.Where(x=>x.Name!="SystemAdmin").ToListAsync();
        }

       
    }
}
