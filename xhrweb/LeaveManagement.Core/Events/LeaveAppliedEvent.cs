using ASL.Hrms.SharedKernel;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;

namespace LeaveManagement.Core.Events
{
    public class LeaveAppliedEvent : BaseDomainEvent
    {
        public LeaveApplication LeaveApplied { get; set; }
        public LeaveAppliedEvent(LeaveApplication leaveApplied)
        {
            LeaveApplied = leaveApplied;
        }
    }
}
