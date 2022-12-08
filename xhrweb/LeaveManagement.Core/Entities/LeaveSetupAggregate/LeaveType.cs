using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Core.Interfaces;
using System;

namespace LeaveManagement.Core.Entities.LeaveSetupAggregate
{
    public class LeaveType : BaseEntity<Guid>, IAggregateRoot, IAuditable
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

        public static LeaveType CreateLeaveType(Guid companyId, string leaveTypeName, 
            string leaveTypeShortName, string leaveTypeLocalizedName, 
            int balance, bool isCarryForward, bool isFemaleSpecific, bool isPaid, 
            bool isEncashable, int encashReserveBalance, bool isDependOnDateOfConfirmation, 
            bool isLeaveDaysFixed, bool isBasedWorkingDays, int consecutiveLimit, 
            bool isAllowAdvanceLeaveApply, bool isAllowWithPrecedingHoliday, 
            bool isAllowOpeningBalance, bool isPreventPostLeaveApply, int leaveTypeGroupId)
        {

            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(leaveTypeName, "leaveTypeName");
            Guard.Against.NullOrWhiteSpace(leaveTypeShortName, "leaveTypeShortName");
            //Guard.Against.NullOrWhiteSpace(leaveTypeLocalizedName, "leaveTypeLocalizedName");
            Guard.Against.NegativeOrZero(balance, "balance");

            // need to validate advance leave apply & post leave apply (both of them will not be in a single type)

            var leaveType = new LeaveType(Guid.NewGuid())
            {
                CompanyId = companyId,
                LeaveTypeName = leaveTypeName,
                LeaveTypeShortName = leaveTypeShortName,
                LeaveTypeLocalizedName = leaveTypeLocalizedName,
                Balance = balance,
                IsCarryForward = isCarryForward,
                IsFemaleSpecific = isFemaleSpecific,
                IsPaid = isPaid,
                IsEncashable = isEncashable,
                EncashReserveBalance = encashReserveBalance,
                IsDependOnDateOfConfirmation = isDependOnDateOfConfirmation,
                IsLeaveDaysFixed = isLeaveDaysFixed,
                IsBasedWorkingDays = isBasedWorkingDays,
                ConsecutiveLimit = consecutiveLimit,
                IsAllowAdvanceLeaveApply = isAllowAdvanceLeaveApply,
                IsAllowWithPrecedingHoliday = isAllowWithPrecedingHoliday,
                IsAllowOpeningBalance = isAllowOpeningBalance,
                IsPreventPostLeaveApply = isPreventPostLeaveApply,
                LeaveTypeGroupId = leaveTypeGroupId
            };

            return leaveType;

        }
        public void UpdateLeaveType(Guid companyId, string leaveTypeName, string leaveTypeShortName, string leaveTypeLocalizedName,
            int balance, bool isCarryForward, bool isFemaleSpecific, bool isPaid, bool isEncashable, int encashReserveBalance,
            bool isDependOnDateOfConfirmation, bool isLeaveDaysFixed, bool isBasedWorkingDays, int consecutiveLimit,
            bool isAllowAdvanceLeaveApply, bool isAllowWithPrecedingHoliday,
            bool isAllowOpeningBalance, bool isPreventPostLeaveApply, int leaveTypeGroupId)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "companyId");
            Guard.Against.NullOrWhiteSpace(leaveTypeName, "leaveTypeName");
            Guard.Against.NullOrWhiteSpace(leaveTypeShortName, "leaveTypeShortName");
            //Guard.Against.NullOrWhiteSpace(leaveTypeLocalizedName, "leaveTypeLocalizedName");
            Guard.Against.NegativeOrZero(balance, "balance");

            CompanyId = companyId;
            LeaveTypeName = leaveTypeName;
            LeaveTypeShortName = leaveTypeShortName;
            LeaveTypeLocalizedName = leaveTypeLocalizedName;
            Balance = balance;
            IsCarryForward = isCarryForward;
            IsFemaleSpecific = isFemaleSpecific;
            IsPaid = isPaid;
            IsEncashable = isEncashable;
            EncashReserveBalance = encashReserveBalance;
            IsDependOnDateOfConfirmation = isDependOnDateOfConfirmation;
            IsLeaveDaysFixed = isLeaveDaysFixed;
            IsBasedWorkingDays = isBasedWorkingDays;
            ConsecutiveLimit = consecutiveLimit;
            IsAllowAdvanceLeaveApply = isAllowAdvanceLeaveApply;
            IsAllowWithPrecedingHoliday = isAllowWithPrecedingHoliday;
            IsAllowOpeningBalance = isAllowOpeningBalance;
            IsPreventPostLeaveApply = isPreventPostLeaveApply;
            LeaveTypeGroupId = leaveTypeGroupId;
        }
        public void MarkLeaveTypeAsDeleted()
        {
            IsDeleted = true;
        }

    }
}
