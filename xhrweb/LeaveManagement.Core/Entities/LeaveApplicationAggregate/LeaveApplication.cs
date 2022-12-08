using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using LeaveManagement.Core.Events;
using LeaveManagement.Core.Enums;
using LeaveManagement.Core.ExtensionMethods;
using System;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Enums;

namespace LeaveManagement.Core.Entities.LeaveApplicationAggregate
{
    public class LeaveApplication : BaseEntity<Guid>, IAuditable
    {
        public Guid CompanyId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid LeaveTypeId { get; private set; }
        public string LeaveCalendar { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public double NoOfDays { get; private set; }
        public string Reason { get; private set; }
        //public LeaveApplicationStatus LeaveApplicationStatus { get; private set; }
        public string ApprovalStatus { get; private set; }
        public string Notes { get; private set; }
        public DateTime ApplyDate { get; private set; }

        public string AddressOnLeave { get; set; }
        public string EmergencyContact { get; set; }
        public Boolean IsHalfDayLeave { get; set; }
        public int? HalfDayLeaveTypeId { get; set; }
        public LeaveApplication(Guid id) : base(id) { }

        // Need for EF
        private LeaveApplication() : base(Guid.NewGuid()) { }

        // Factory method for create
        public void StartLeaveApplication(Guid companyId, Guid employeeId, Guid leaveTypeId, string leaveCalendar,
            DateTime startDate, DateTime endDate, double noOfDays, string reason, Boolean isHalfDayLeave, int? halfDayLeaveTypeId,
            string addressOnLeave, string emergencyContact)
        {
            Guard.Against.NullOrEmptyGuid(companyId, nameof(companyId));
            Guard.Against.NullOrEmptyGuid(employeeId, nameof(employeeId));
            Guard.Against.NullOrEmptyGuid(leaveTypeId, nameof(leaveTypeId));
            Guard.Against.NullOrEmpty(leaveCalendar, nameof(leaveCalendar));
            Guard.Against.Null(startDate, nameof(startDate));
            Guard.Against.Null(endDate, nameof(endDate));
            Guard.Against.InvalidLeaveDate(startDate, endDate, nameof(EndDate), "End date should be greater or equal than start date.");

            CompanyId = companyId;
            EmployeeId = employeeId;
            LeaveTypeId = leaveTypeId;
            LeaveCalendar = leaveCalendar;
            StartDate = startDate;
            EndDate = endDate;
            NoOfDays = noOfDays;
            Reason = reason;
            IsHalfDayLeave = isHalfDayLeave;
            HalfDayLeaveTypeId = halfDayLeaveTypeId;
            AddressOnLeave = addressOnLeave;
            EmergencyContact = emergencyContact;
            ApprovalStatus = ((int)ApprovalStatuses.Pending).ToString();// LeaveApplicationStatus.PendingApproval;

            Events.Add(new LeaveAppliedEvent(this));
        }

        public void UpdateLeaveApplication(Guid leaveTypeId, string leaveCalendar, DateTime startDate,
            DateTime endDate, double noOfDays, string reason, Boolean isHalfDayLeave, int? halfDayLeaveTypeId,
            string addressOnLeave, string emergencyContact)
        {
            Guard.Against.InvalidApplicationStatus(((int)ApprovalStatuses.Pending).ToString(), ((int)ApprovalStatuses.Approved).ToString(), nameof(LeaveApplicationStatus), "Cannot update approved application");

            LeaveTypeId = leaveTypeId;
            LeaveCalendar = leaveCalendar;
            StartDate = startDate;
            EndDate = endDate;
            NoOfDays = noOfDays;
            Reason = reason;
            IsHalfDayLeave = isHalfDayLeave;
            HalfDayLeaveTypeId = halfDayLeaveTypeId;
            AddressOnLeave = addressOnLeave;
            EmergencyContact = emergencyContact;
        }

        public void ApproveLeaveApplication(string notes = "")
        {
            Guard.Against.InvalidApplicationStatus(ApprovalStatus, ((int)ApprovalStatuses.Approved).ToString(), nameof(LeaveApplicationStatus), "already approved application");
            ApprovalStatus = ((int)ApprovalStatuses.Approved).ToString();// LeaveApplicationStatus.Approved;
            Notes = notes;
        }

        public void RejectLeaveApplication(string notes = "")
        {
            Guard.Against.InvalidApplicationStatus(ApprovalStatus, ((int)ApprovalStatuses.Approved).ToString(), nameof(LeaveApplicationStatus), "already approved application");
            Guard.Against.InvalidApplicationStatus(ApprovalStatus, ((int)ApprovalStatuses.Declined).ToString(), nameof(LeaveApplicationStatus), "already declined application");
            ApprovalStatus = ((int)ApprovalStatuses.Declined).ToString();// LeaveApplicationStatus.Rejected;
            Notes = notes;
        }
    }
}
