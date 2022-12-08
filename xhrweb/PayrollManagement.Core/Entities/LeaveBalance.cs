using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities
{
    public class LeaveBalance : BaseEntity<Guid>
    {
        public Guid CompanyId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid LeaveTypeId { get; private set; }
        public string LeaveCalendar { get; private set; }
        public double MaximumBalance { get; private set; }
        public double UsedBalance { get; private set; }
        public double RemainingBalance { get; private set; }
        public bool IsDeleted { get; private set; }

        public LeaveBalance(Guid id) : base(id) { }

        // Need for EF
        private LeaveBalance() : base(Guid.NewGuid()) { }
    }
}
