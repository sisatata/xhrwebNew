using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.ApiModels.AppAuth
{
    public class UserRoleDTO
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
