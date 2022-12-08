using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Hrms.SharedKernel.Models
{
   public class UserVM
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string UserId { get; set; }
    }
}
