using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class AttendanceProcessedDataFilterSpecification : BaseSpecification<AttendanceProcessedData>
    {
        public AttendanceProcessedDataFilterSpecification(Guid companyId, Boolean IsDeleted)
            : base(x => x.Id != null)
        {

        }
    }
}