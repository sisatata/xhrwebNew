using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class AttendanceFilterSpecification : BaseSpecification<AttendanceCommon>
    {
        public AttendanceFilterSpecification(Guid companyId, DateTime StartDate, DateTime EndDate, Boolean IsDeleted)

            : base(x => x.IsDeleted == false && x.AttendanceDate >= StartDate && x.AttendanceDate <= EndDate.AddDays(1))
        {

        }
    }
}