using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class EmployeeWiseCustomConfigurationFilterSpecification : BaseSpecification<EmployeeWiseCustomConfiguration>
    {
        public EmployeeWiseCustomConfigurationFilterSpecification(Guid companyId, Guid employeeId)

            : base(x => x.CompanyId == companyId && x.EmployeeId == employeeId)
        {

        }
    }
}