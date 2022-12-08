using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
    public class EmployeeCustomConfigurationByCompanyFilterSpecificaion : BaseSpecification<EmployeeCustomConfiguration>

    {
        public EmployeeCustomConfigurationByCompanyFilterSpecificaion(Guid companyId, string code)
            : base(x => x.CompanyId == companyId && x.Code.Trim().ToLower() == code.Trim().ToLower())
        {
        }
    }
}
