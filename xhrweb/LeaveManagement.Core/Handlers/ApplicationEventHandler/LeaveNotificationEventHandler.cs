using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Core.ApplicationEvents;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LeaveManagement.Core.Handlers.ApplicationEventHandler
{
    public class LeaveNotificationEventHandler : ICommandHandler<LeaveNotificationEvent>
    {
        public LeaveNotificationEventHandler(IConfiguration configuration, 
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(LeaveNotificationEvent command)
        {
            await _pushNotificationService.SendLeaveApplyNotification(command.ManagerId, command.EmployeeId, command.NoOfDays, command.ApplicationId
                , command.StartDate, command.EndDate, command.Reason, command.IsHalfDayLeave);
        }
    }
}
