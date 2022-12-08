using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface ILeaveEncashmentService
    {
        Task<bool> AdjustLeaveBalanceAfterEncashment(Guid? CompanyId, Guid? EmployeeId, Guid LeaveTypeId, string LeaveCalendar, decimal? LeaveEncashedDays);
    }
}
