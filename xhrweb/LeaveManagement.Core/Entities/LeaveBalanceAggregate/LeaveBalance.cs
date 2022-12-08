using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;
using System;

namespace LeaveManagement.Core.Entities.LeaveBalanceAggregate
{
    public class LeaveBalance : BaseEntity<Guid>, IAuditable
    {
        public Guid CompanyId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid LeaveTypeId { get; private set; }
        public string LeaveCalendar { get; private set; }
        public double MaximumBalance { get; private set; }
        public double UsedBalance { get; private set; }
        public double RemainingBalance { get; private set; }
        public bool IsDeleted { get; private set; }

        // not persisted
        public TrackingState State { get; set; }

        public LeaveBalance(Guid id) : base(id) { }

        // Need for EF
        private LeaveBalance() : base(Guid.NewGuid()) { }

        // Factory method for create
        public static LeaveBalance CreateLeaveBalance(Guid companyId, Guid employeeId, Guid leaveTypeId, string leaveCalendar, double maximumBalance,
            double usedBalance, double remainingBalance)
        {
            Guard.Against.NullOrEmptyGuid(companyId, nameof(companyId));
            Guard.Against.NullOrEmptyGuid(employeeId, nameof(employeeId));
            Guard.Against.NullOrEmptyGuid(leaveTypeId, nameof(leaveTypeId));
            Guard.Against.NullOrWhiteSpace(leaveCalendar, nameof(leaveCalendar));



            var leaveBalance = new LeaveBalance(Guid.NewGuid())
            {
                CompanyId = companyId,
                EmployeeId = employeeId,
                LeaveTypeId = leaveTypeId,
                LeaveCalendar = leaveCalendar,
                MaximumBalance = maximumBalance,
                UsedBalance = usedBalance,
                RemainingBalance = remainingBalance
            };

            return leaveBalance;
        }

        public void AdjustBalance(double maximumBalance, double usedBalance, double remainingBalance)
        {

            if (MaximumBalance == maximumBalance && UsedBalance == usedBalance && RemainingBalance == remainingBalance) return;

            MaximumBalance = maximumBalance;
            UsedBalance = usedBalance;
            RemainingBalance = remainingBalance;
        }
        public void AdjustBalance(double usedBalance)
        {

            //if (MaximumBalance == maximumBalance && UsedBalance == usedBalance && RemainingBalance == remainingBalance) return;

            //MaximumBalance = maximumBalance;
            UsedBalance = UsedBalance + usedBalance;
            RemainingBalance = RemainingBalance - usedBalance;
            State = TrackingState.Modified;
        }

        public void UpdateBalance(double maximumBalance)
        {
            if (MaximumBalance == maximumBalance) return;
            MaximumBalance = maximumBalance;
            RemainingBalance = maximumBalance - UsedBalance;
        }

        public void DeleteLeaveBalance()
        {
            IsDeleted = true;
            State = TrackingState.Deleted;
        }
    }
}
