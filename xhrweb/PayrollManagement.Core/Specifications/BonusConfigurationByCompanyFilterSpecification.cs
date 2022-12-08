using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class BonusConfigurationByCompanyFilterSpecification : BaseSpecification<BonusConfiguration>
    {
        public BonusConfigurationByCompanyFilterSpecification(Guid companyId)
            : base(x => x.CompanyId == companyId && x.IsDeleted == false)
        {
        }
    }
}
