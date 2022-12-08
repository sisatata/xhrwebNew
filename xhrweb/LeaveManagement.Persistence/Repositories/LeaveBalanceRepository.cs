using ASL.Hrms.SharedKernel.Enums;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using LeaveManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LeaveManagement.Persistence.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        public LeaveBalanceRepository(LeaveContext leaveContext)
        {
            _leaveContext = leaveContext;
        }

        private readonly LeaveContext _leaveContext;
        
        public async Task Update(LeaveBalanceAggregate leaveBalance)
        {
            foreach (var balance in leaveBalance.LeaveBalances)
            {
                if(balance.State == TrackingState.Added)
                {
                    _leaveContext.Entry(balance).State = EntityState.Added;
                }
                if (balance.State == TrackingState.Modified)
                {
                    _leaveContext.Entry(balance).State = EntityState.Modified;
                }
                if (balance.State == TrackingState.Deleted)
                {
                    _leaveContext.Entry(balance).State = EntityState.Deleted;
                }
            }
            await _leaveContext.SaveChangesAsync();
        }
    }
}
