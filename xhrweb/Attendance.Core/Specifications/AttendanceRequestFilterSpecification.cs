using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class AttendanceRequestFilterSpecification : BaseSpecification<AttendanceRequest>
    {
        public AttendanceRequestFilterSpecification(Guid companyId, DateTime StartDate, DateTime EndDate, Boolean IsDeleted)

            : base(x => x.IsDeleted == false && x.StartTime >= StartDate && x.StartTime <= EndDate && x.CompanyId == companyId)
        {

        }
    }
}