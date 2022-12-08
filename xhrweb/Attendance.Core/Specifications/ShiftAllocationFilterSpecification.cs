using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class ShiftAllocationFilterSpecification : BaseSpecification<ShiftAllocation>
    {
        public ShiftAllocationFilterSpecification(Guid companyId, Boolean IsDeleted)
            : base(x => x.CompanyId == companyId && x.IsDeleted == IsDeleted)
        {

        }
    }
}