using ASL.AccessControl.Domain;
using System.Collections.Generic;

namespace ASL.AccessControl.Services.Security
{
    public interface IPermissionProvider
    {
        IEnumerable<PermissionRecord> GetPermissions();
    }
}
