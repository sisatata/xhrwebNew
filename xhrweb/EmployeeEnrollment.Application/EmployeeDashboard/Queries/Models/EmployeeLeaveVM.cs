using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models
{
    public class EmployeeLeaveVM
    {
        //public Guid EmployeeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveTypeShortName { get; set; }
        public decimal MaximumBalance { get; set; }
        public decimal UsedBalance { get; set; }
        public decimal RemainingBalance { get; set; }
        public string LeaveCalendar { get; set; }
    }
}
