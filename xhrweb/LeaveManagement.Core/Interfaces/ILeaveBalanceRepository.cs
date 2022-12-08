using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Core.Interfaces
{
    public interface ILeaveBalanceRepository
    {
        Task Update(LeaveBalanceAggregate leaveBalance);
    }
}
