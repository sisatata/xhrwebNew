using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class BonusConfigurationByCompanyAndActiveFilterSpecification : BaseSpecification<BonusConfiguration>

    {
        public BonusConfigurationByCompanyAndActiveFilterSpecification(Guid companyId,  DateTime bonusProcessDate)
            : base(x => x.CompanyId == companyId && x.StartDate < bonusProcessDate.Date && x.EndDate >= bonusProcessDate.Date)
        {
        }
    }
}
