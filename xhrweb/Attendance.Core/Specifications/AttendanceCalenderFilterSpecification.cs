using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
   public class AttendanceCalenderFilterSpecification : BaseSpecification<AttendanceCalendar>
    {
        public AttendanceCalenderFilterSpecification(Guid companyId, DateTime leaveDate)
            : base(x => x.YearStartDate <= leaveDate && x.YearEndDate >= leaveDate && x.CompanyId == companyId)
        {

        }
    }
}