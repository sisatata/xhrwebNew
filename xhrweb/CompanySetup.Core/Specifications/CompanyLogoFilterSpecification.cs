using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
   public class CompanyLogoFilterSpecification : BaseSpecification<Company>
    {
        public CompanyLogoFilterSpecification(Guid? companyId)

            : base(x=>x.Id == companyId &&   x.IsDeleted == false)
        {

        }
    }
}
