using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class ConfirmationRule : BaseEntity<Guid>
    {
        public string RuleName { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Int16? ConfirmationType { get; private set; }
        public Int16? ConfirmationMonths { get; private set; }
        public Int16? SortOrder { get; private set; }
        public Boolean IsActive { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public ConfirmationRule(Guid id) : base(id) { }
        private ConfirmationRule() : base(Guid.NewGuid()) { }

    }
}

