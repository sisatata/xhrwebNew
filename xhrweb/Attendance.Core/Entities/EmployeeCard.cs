using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
    public class EmployeeCard : BaseEntity<Guid>
    {
        public Guid EmployeeId { get; private set; }
        public string CardMaskingValue { get; private set; }
        public DateTime? IssueDate { get; private set; }
        public decimal? ChargeAmount { get; private set; }
        public Boolean IsPaid { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public Boolean CardRevoked { get; private set; }
        public DateTime? CardRevokedDate { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public EmployeeCard(Guid id) : base(id) { }
        private EmployeeCard() : base(Guid.NewGuid()) { }
    }
}
