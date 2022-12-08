using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.ApiModels.AppAuth
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
