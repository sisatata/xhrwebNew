using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.ApiModels.AppAuth
{
    public class RolePermissionDTO
    {
        public string[] Roles { get; set; }
        public string[] Permissions { get; set; }
    }
}
