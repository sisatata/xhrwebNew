using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.Models
{
    public class UserRoles
    {
        public Guid? UserId { get; set; }
        public Guid? Id { get; set; }
        public Guid? RoleId { get; set; }
        public string ItemName { get; set; }
        public string EmployeeName { get; set; }
    }
}
