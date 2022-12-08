using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmployeeEnrollment.Core.Specifications
{
    public class EmployeeStatusByCompanyFilterSpecification : BaseSpecification<EmployeeStatus>
    {
        public EmployeeStatusByCompanyFilterSpecification(Guid companyId)
            : base(r => r.CompanyId == companyId && r.IsDeleted == false)
        {

        }
    }
}
