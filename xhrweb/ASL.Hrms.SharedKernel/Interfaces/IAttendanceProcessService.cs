using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IAttendanceProcessService
    {
        Task<bool> AttendanceProcess(Guid? ManagerId, Guid? EmployeeId, DateTime? StartDate, DateTime? EndDate);
    }
}
