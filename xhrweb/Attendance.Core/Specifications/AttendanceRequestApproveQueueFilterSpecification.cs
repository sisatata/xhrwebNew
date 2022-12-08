using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class AttendanceRequestApproveQueueFilterSpecification : BaseSpecification<AttendanceRequestApproveQueue>
    {
        public AttendanceRequestApproveQueueFilterSpecification(Guid attendanceRequestId, Guid managerId)
            : base(x => x.AttendanceApplicationId == attendanceRequestId && x.ManagerId == managerId)
        {
        }
    }
}
