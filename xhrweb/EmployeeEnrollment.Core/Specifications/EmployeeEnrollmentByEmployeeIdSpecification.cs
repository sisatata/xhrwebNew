using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmployeeEnrollment.Core.Specifications
{
    public class EmployeeEnrollmentByEmployeeIdSpecification : BaseSpecification<EmployeeEnrollment.Core.Entities.EmployeeEnrollment>
    {
        public EmployeeEnrollmentByEmployeeIdSpecification(Guid employeeId)
            : base(r => r.EmployeeId == employeeId && r.IsDeleted == false)
        {

        }
    }
}
