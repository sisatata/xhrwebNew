using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class EmployeeCardByCompanyFilterSpecification : BaseSpecification<EmployeeCard>
    {
        public EmployeeCardByCompanyFilterSpecification(Guid companyId)

            : base(x => x.IsDeleted == false && x.CompanyId == companyId)
        {

        }
    }
}