using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class IncomeTaxParameterFilterSpecification : BaseSpecification<IncomeTaxParameter>
    {
        public IncomeTaxParameterFilterSpecification(Guid companyId, DateTime startDate, DateTime endDate)
            : base(x => x.CompanyId == companyId && x.StartDate <= startDate && x.EndDate >= endDate && x.IsDeleted == false)
        {

        }
    }
}