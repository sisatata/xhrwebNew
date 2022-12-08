using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
    }
}
