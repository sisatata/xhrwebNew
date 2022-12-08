using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class ShiftGroupFilterSpecification : BaseSpecification<ShiftGroup>
    {
        public ShiftGroupFilterSpecification(Guid? companyId)
            : base(x => (x.CompanyId == companyId || companyId == null || companyId == Guid.Empty) && x.IsDeleted == false)
        {

        }
    }
}