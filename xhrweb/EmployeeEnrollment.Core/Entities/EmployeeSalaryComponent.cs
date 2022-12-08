using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeSalaryComponent : BaseEntity<Guid>
    {
        public Guid? EmployeeSalaryId { get; set; }
        public Guid? SalaryStructureComponentId { get; private set; }
        public decimal? ComponentAmount { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }
        //public string DisplayName { get; private set; }
    }
}
