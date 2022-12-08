using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Entities
{
    public class CompanyOrganogram : BaseEntity<Guid>
    {
        public Guid DesignationId { get; private set; }
        public string DesignationName { get; private set; }
        public Guid DepartmentId { get; private set; }
        public string DepartmentName { get; private set; }
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }
        public Guid CompanyId { get; private set; }
        public string CompanyName { get; private set; }
        public Guid GradeId { get; private set; }
        public string GradeName { get; private set; }


        public CompanyOrganogram(Guid id) : base(id) { }
        private CompanyOrganogram() : base(Guid.NewGuid()) { }
    }
}
