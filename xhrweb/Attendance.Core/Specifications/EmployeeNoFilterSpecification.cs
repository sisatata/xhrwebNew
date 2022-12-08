using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class EmployeeNoFilterSpecification : BaseSpecification<Employee>
    {
        public EmployeeNoFilterSpecification(Guid? companyId)
            : base(x => x.CompanyId == companyId || companyId == null || companyId == Guid.Empty)
        {

        }
    }
}