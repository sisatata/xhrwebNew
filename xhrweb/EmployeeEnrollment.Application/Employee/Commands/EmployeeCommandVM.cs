using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    public class EmployeeCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}



