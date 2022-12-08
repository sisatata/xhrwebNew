using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveTypes.Commands.Models
{
    public class LeaveTypeCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
