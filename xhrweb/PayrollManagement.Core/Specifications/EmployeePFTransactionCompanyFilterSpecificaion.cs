using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeePFTransactionCompanyFilterSpecificaion : BaseSpecification<EmployeePFTransaction>

    {
        public EmployeePFTransactionCompanyFilterSpecificaion(Guid companyId)
            : base(x => x.CompanyId == companyId && x.IsDeleted == false)
        {
        }
    }
}
