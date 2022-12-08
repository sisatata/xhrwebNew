using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveApplications.Queries.Models
{
   public class LeaveApplicationSummaryVM
    {
        public Guid EmployeeId { get; set; }
        public List<EmployeeLeaveVM> EmployeeLeaves { get; set; }
        public List<LeaveApplicationVM> EmployeeLeaveApplictions { get; set; }
    }
}
