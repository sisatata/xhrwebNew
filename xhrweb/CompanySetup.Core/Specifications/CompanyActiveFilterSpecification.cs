using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class CompanyActiveFilterSpecification : BaseSpecification<Company>
    {
        public CompanyActiveFilterSpecification()

            : base(x => x.IsDeleted == false && x.ApprovalStatus == ((int)ApprovalStatuses.Approved).ToString())
        {

        }
    }
}