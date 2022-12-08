using System;

namespace LeaveManagement.Application.LeaveApplications.Commands.Models
{
    public class LeaveApplicationVM
    {
        public Guid ApplicationId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
