using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    public class EmployeeDeleteCommandVM
    {
        public Guid Id { get; set; }
        public Guid LoginId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}



