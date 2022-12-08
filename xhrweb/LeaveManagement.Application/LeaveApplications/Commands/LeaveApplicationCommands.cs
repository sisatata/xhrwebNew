using LeaveManagement.Application.LeaveApplications.Commands.Models;
using MediatR;
using System;

namespace LeaveManagement.Application.LeaveApplications.Commands.V1
{
    public class ApplyLeaveCommand : IRequest<LeaveApplicationVM>
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string LeaveCalendar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public double LeaveDays { get; set; }
        public string Reason { get; set; }
        public string AddressOnLeave { get; set; }
        public string EmergencyContact { get; set; }
        public Boolean IsHalfDayLeave { get; set; }
        public int? HalfDayLeaveTypeId { get; set; }
    }

    public class UpdateLeaveCommand : IRequest<LeaveApplicationVM>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string LeaveCalendar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public double LeaveDays { get; set; }
        public string Reason { get; set; }
        public string AddressOnLeave { get; set; }
        public string EmergencyContact { get; set; }
        public Boolean IsHalfDayLeave { get; set; }
        public int? HalfDayLeaveTypeId { get; set; }
    }
    public class ApproveLeaveCommand : IRequest<LeaveApplicationVM>
    {
        public Guid ApplicationId { get; set; }
        public string Notes { get; set; }
    }
    public class RejectLeaveCommand : IRequest<LeaveApplicationVM>
    {
        public Guid ApplicationId { get; set; }
        public string Notes { get; set; }
    }
}
