using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Core.ApplicationEvents;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LeaveManagement.Core.Handlers.ApplicationEventHandler
{
    public class LeaveApprovedEventHandler : ICommandHandler<LeaveApprovedEvent>
    {
        public LeaveApprovedEventHandler(IConfiguration configuration, 
            IPushNotificationService pushNotificationService, IAttendanceProcessService attendanceProcessService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
            _attendanceProcessService = attendanceProcessService;

        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IAttendanceProcessService _attendanceProcessService;
        public async Task Handle(LeaveApprovedEvent command)
        {
            await _pushNotificationService.SendLeaveApproveNotification(command.ManagerId, command.EmployeeId, command.NoOfDays, command.ApplicationId, command.StartDate, command.EndDate, command.Note, command.IsHalfDayLeave);
            await _attendanceProcessService.AttendanceProcess(command.CompanyId, command.EmployeeId, command.StartDate, command.EndDate);
        }
    }
}
