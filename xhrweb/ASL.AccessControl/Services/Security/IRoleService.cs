using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASL.AccessControl.Services.Security
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetRoles();
        Task<bool> CreateRole(string roleName);
        Task<bool> AssignRolePermission(string[] roleIds, string[] permissions);
    }
}
