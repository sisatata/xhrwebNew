using System;
using ASL.Hrms.SharedKernel.Interfaces;

namespace PayrollManagement.Core.ApplicationEvents
{
    public class LeaveEncashmentEvent : ICommand
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string LeaveCalendar { get; set; }
        public decimal? LeaveEncashedDays { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
