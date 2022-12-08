using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class ShiftFilterByShiftGroupIdSpecification : BaseSpecification<Shift>
    {
        public ShiftFilterByShiftGroupIdSpecification(Guid? companyId, Guid? shiftGroupId)
            : base(x => (x.CompanyId == companyId || companyId == null || companyId == Guid.Empty)
            && (x.ShiftGroupID == shiftGroupId || shiftGroupId == null || shiftGroupId == Guid.Empty)
            && x.IsDeleted == false && x.IsActive == true)
        {

        }
    }
}