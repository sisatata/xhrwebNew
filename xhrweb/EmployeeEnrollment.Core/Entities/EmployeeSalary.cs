using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeSalary : BaseEntity<Guid>
    {
        public Guid? EmployeeId { get; private set; }
        public Guid? BranchId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public Guid? PositionId { get; private set; }
        public Guid? GradeId { get; private set; }
        public Guid? SalaryStructureId { get; private set; }
        public Int16? PaymentOptionId { get; private set; }
        public decimal? GrossSalary { get; private set; }
        public Guid? CompanyId { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public EmployeeSalary(Guid id) : base(id) { }
        private EmployeeSalary() : base(Guid.NewGuid()) { }

    }
}
