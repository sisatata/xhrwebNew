using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class DepartmentFilterSpecification : BaseSpecification<Department>
    {
        public DepartmentFilterSpecification(Guid companyId)

            : base(x => x.IsDeleted == false && x.CompanyId == companyId)
        {
        }
    }
}