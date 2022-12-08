using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities
{
    public class LeaveType : BaseEntity<Guid>
    {
        public Guid CompanyId { get; private set; }
        public string LeaveTypeName { get; private set; }
        public string LeaveTypeShortName { get; private set; }
        public string LeaveTypeLocalizedName { get; private set; }
        public int Balance { get; private set; }
        public bool IsCarryForward { get; private set; }
        public bool IsFemaleSpecific { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsEncashable { get; private set; }
        public int EncashReserveBalance { get; private set; }
        public bool IsDependOnDateOfConfirmation { get; private set; }
        public bool IsLeaveDaysFixed { get; private set; }
        public bool IsBasedWorkingDays { get; private set; }
        public int ConsecutiveLimit { get; private set; }
        public bool IsAllowAdvanceLeaveApply { get; private set; }
        public bool IsAllowWithPrecedingHoliday { get; private set; }
        public bool IsAllowOpeningBalance { get; private set; }
        public bool IsPreventPostLeaveApply { get; private set; }
        public bool IsDeleted { get; private set; }
        public int LeaveTypeGroupId { get; private set; }
        public LeaveType(Guid id) : base(id) { }

        // Need for EF
        private LeaveType() : base(Guid.NewGuid()) { }
    }
}
