using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Core.ApplicationEvents;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LeaveManagement.Core.Handlers.ApplicationEventHandler
{
    public class LeaveDeclinedEventHandler : ICommandHandler<LeaveDeclinedEvent>
    {
        public LeaveDeclinedEventHandler(IConfiguration configuration, 
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(LeaveDeclinedEvent command)
        {
            await _pushNotificationService.SendLeaveDeclineNotification(command.ManagerId, command.EmployeeId, command.NoOfDays, command.ApplicationId, command.StartDate, command.EndDate, command.Note, command.IsHalfDayLeave);
        }
    }
}
