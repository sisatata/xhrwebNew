using ASL.AccessControl.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.Services.Security
{
    public abstract class DefaultPermissionProvider
    {
        //admin area permissions
        public static readonly PermissionRecord ViewUser = new PermissionRecord { Name = "View User", SystemName = "Permissions.ViewUser", Category = "User Management" };
        public static readonly PermissionRecord CreateUser = new PermissionRecord { Name = "Create User", SystemName = "Permissions.CreateUser", Category = "User Management" };
        public static readonly PermissionRecord EditUser = new PermissionRecord { Name = "Edit User", SystemName = "Permissions.EditUser", Category = "User Management" };
        public static readonly PermissionRecord DeleteUser = new PermissionRecord { Name = "Delete User", SystemName = "Permissions.DeleteUser", Category = "User Management" };

        public const string ViewUserPermission = "Permissions.ViewUser";

        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                ViewUser,
                CreateUser,
                EditUser,
                DeleteUser
            };
        }
    }
}
