using System;

namespace ASL.AccessControl.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
