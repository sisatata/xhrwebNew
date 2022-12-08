using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class ShiftFilterSpecification : BaseSpecification<Shift>
    {
        public ShiftFilterSpecification(Guid companyId, Boolean IsDeleted)
            : base(x => x.CompanyId == companyId && x.IsDeleted == IsDeleted && x.IsActive == true)
        {

        }
    }
}