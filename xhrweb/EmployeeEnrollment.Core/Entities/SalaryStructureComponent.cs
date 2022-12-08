using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Entities
{
    public class SalaryStructureComponent : BaseEntity<Guid>
    {
        public Guid? SalaryStrutureId { get; private set; }
        public string SalaryComponentName { get; private set; }
        public decimal? Value { get; private set; }
        public string ValueType { get; private set; }
        public string PercentOn { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }
        public short SortOrder { get; private set; }

        public SalaryStructureComponent(Guid id) : base(id) { }
        private SalaryStructureComponent() : base(Guid.NewGuid()) { }

    }
}
