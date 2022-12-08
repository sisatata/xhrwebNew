using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class IncomeTaxPayerTypeFilterSpecification : BaseSpecification<IncomeTaxPayerType>
    {
        public IncomeTaxPayerTypeFilterSpecification(Guid companyId)
            : base(x => x.CompanyId == companyId && x.IsActive == true && x.IsDeleted == false)
        {

        }
    }
}