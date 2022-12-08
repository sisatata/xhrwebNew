using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class EmployeeAttendanceManagerSpecificaion : BaseSpecification<EmployeeManager>

    {
        public EmployeeAttendanceManagerSpecificaion(Guid employeeId)
            : base(x => x.EmployeeId == employeeId)
        {
        }
    }
}
