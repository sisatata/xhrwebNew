using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
   public class BranchFilterSpecification : BaseSpecification<Branch>
    {
        public BranchFilterSpecification(Guid companyId)

            : base(x => x.IsDeleted == false && x.CompanyId == companyId)
        {
        }

    }
}
