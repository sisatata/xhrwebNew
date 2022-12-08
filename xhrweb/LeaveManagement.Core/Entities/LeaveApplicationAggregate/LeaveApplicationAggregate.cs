using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Events;
using LeaveManagement.Core.ExtensionMethods;
using LeaveManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveManagement.Core.Entities.LeaveApplicationAggregate
{
    public class LeaveApplicationAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        public LeaveApplicationAggregate(Guid companyId, Guid employeeId, LeaveType leaveType, LeaveBalance leaveBalance,
            IReadOnlyList<LeaveApplication> employeeLeaveApplicationData)
        {
            Guard.Against.NullOrEmptyGuid(companyId, nameof(companyId));
            Guard.Against.NullOrEmptyGuid(employeeId, nameof(employeeId));
            Guard.Against.Null(leaveType, nameof(leaveType));
            Guard.Against.Null(leaveBalance, nameof(leaveBalance));

            CompanyId = companyId;
            EmployeeId = employeeId;
            LeaveType = leaveType;
            LeaveBalance = leaveBalance;
            LeaveApplication = new LeaveApplication(Guid.NewGuid());
            LeaveApplicationApproveQueue = new LeaveApplicationApproveQueue(Guid.NewGuid());
            _employeeLeaveApplicationData = employeeLeaveApplicationData;
        }

        public LeaveApplicationAggregate(LeaveApplication leaveApplication)
        {
            LeaveApplication = leaveApplication;
        }

        public Guid CompanyId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public LeaveType LeaveType { get; private set; }
        public LeaveBalance LeaveBalance { get; private set; }
        public LeaveApplication LeaveApplication { get; set; }
        private IReadOnlyList<LeaveApplication> _employeeLeaveApplicationData { get; set; }
        public LeaveApplicationApproveQueue LeaveApplicationApproveQueue { get; set; }


        public void NewLeaveApplication(DateTime startDate, DateTime endDate, double leaveDays, string leaveCalendar, string reason, Boolean isHalfDayLeave, int? halfDayLeaveTypeId,
            string addressOnLeave, string emergencyContact)
        {
            Guard.Against.DuplicateRecords(_employeeLeaveApplicationData.ToList(), startDate, endDate, "Duplicate start/end date");
            Guard.Against.InsufficientBalance(LeaveBalance.RemainingBalance, leaveDays, nameof(leaveDays), "Insufficient balance");
            Guard.Against.AdvanceLeaveApplication(LeaveType.IsAllowAdvanceLeaveApply, startDate, nameof(startDate), "Advance application is not allowed");
            Guard.Against.PostLeaveApplication(LeaveType.IsPreventPostLeaveApply, startDate, nameof(startDate), "Post application is not allowed");

            LeaveApplication.StartLeaveApplication(CompanyId, EmployeeId, LeaveType.Id, leaveCalendar, startDate, endDate, leaveDays, reason, isHalfDayLeave, halfDayLeaveTypeId,
             addressOnLeave, emergencyContact);

        }

        public void UpdateLeaveApplication(LeaveApplication leaveApplication, DateTime startDate, DateTime endDate, double leaveDays, string leaveCalendar, string reason, Boolean isHalfDayLeave, int? halfDayLeaveTypeId,
            string addressOnLeave, string emergencyContact)
        {
            Guard.Against.DuplicateRecordInUpdate(_employeeLeaveApplicationData.ToList(), leaveApplication.Id, startDate, endDate, "Duplicate start/end date");
            Guard.Against.InsufficientBalance(LeaveBalance.RemainingBalance, leaveDays, nameof(leaveDays), "Insufficient balance");
            Guard.Against.AdvanceLeaveApplication(LeaveType.IsAllowAdvanceLeaveApply, startDate, nameof(startDate), "Advance application is not allowed");
            Guard.Against.PostLeaveApplication(LeaveType.IsPreventPostLeaveApply, startDate, nameof(startDate), "Post application is not allowed");
            leaveApplication.UpdateLeaveApplication(LeaveType.Id, leaveCalendar, startDate, endDate, leaveDays, reason, isHalfDayLeave, halfDayLeaveTypeId,
             addressOnLeave, emergencyContact);
            LeaveApplication = leaveApplication;

        }

        public void ApproveLeaveApplication(string notes = "")
        {
            LeaveApplication.ApproveLeaveApplication(notes);
        }
        public void RejectLeaveApplication(string notes = "")
        {
            LeaveApplication.RejectLeaveApplication(notes);
        }

        public void CalculateLeaveDays()
        {
            // complex logic will be added here
            // 1. sandwitch leave
            // 2. remove weekend from start and end date
        }
    }
}
