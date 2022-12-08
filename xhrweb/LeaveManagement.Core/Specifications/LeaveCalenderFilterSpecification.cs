using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
    public class LeaveCalenderFilterSpecification : BaseSpecification<LeaveCalendar>
    {
        public LeaveCalenderFilterSpecification(Guid companyId, DateTime leaveDate)
            : base(x => x.YearStartDate <= leaveDate && x.YearEndDate >= leaveDate && x.CompanyId == companyId)
        {

        }
    }
}
