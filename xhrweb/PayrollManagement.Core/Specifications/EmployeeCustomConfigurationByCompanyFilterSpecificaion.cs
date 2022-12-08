using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeCustomConfigurationByCompanyFilterSpecificaion : BaseSpecification<EmployeeCustomConfiguration>

    {
        public EmployeeCustomConfigurationByCompanyFilterSpecificaion(Guid companyId)
            : base(x => x.CompanyId == companyId)
        {
        }
    }
}
