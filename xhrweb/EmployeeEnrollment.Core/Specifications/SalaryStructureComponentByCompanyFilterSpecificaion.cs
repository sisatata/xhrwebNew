using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Specifications
{
    public class SalaryStructureComponentByCompanyFilterSpecificaion : BaseSpecification<SalaryStructureComponent>

    {
        public SalaryStructureComponentByCompanyFilterSpecificaion(Guid companyId)
            : base(x => x.CompanyId == companyId && x.IsDeleted == false)
        {
        }
    }
}
