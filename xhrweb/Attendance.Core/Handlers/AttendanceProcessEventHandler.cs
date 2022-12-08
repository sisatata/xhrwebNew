using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Handlers
{
    public class AttendanceProcessEventHandler : ICommandHandler<AttendanceProcessEvent>
    {
        public AttendanceProcessEventHandler(IConfiguration configuration, IAttendanceProcessService attendanceProcessService)
        {
            _configuration = configuration;
            _attendanceProcessService = attendanceProcessService;
        }
        private readonly IConfiguration _configuration;
        private readonly IAttendanceProcessService _attendanceProcessService;
        public async Task Handle(AttendanceProcessEvent command)
        {
            await _attendanceProcessService.AttendanceProcess(command.CompanyId, command.EmployeeId, command.StartDate, command.EndDate);
        }
    }
}
