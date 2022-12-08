using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
    public class AttendanceProcessDataFilterSpecificaion : BaseSpecification<AttendanceProcessedData>

    {
        public AttendanceProcessDataFilterSpecificaion()
            : base(x => x.EmployeeId != null)
        {
        }
    }
}
