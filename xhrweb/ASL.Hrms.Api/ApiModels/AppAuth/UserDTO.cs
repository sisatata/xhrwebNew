using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.ApiModels.AppAuth
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
