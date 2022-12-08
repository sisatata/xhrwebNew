using Asl.Infrastructure.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Application.AttendanceProcessedData.Commands;
using Attendance.Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asl.Infrastructure
{
    public class AttendanceProcessService : IAttendanceProcessService
    {
        public AttendanceProcessService(IConfiguration configuration, IMediator mediator
            //,ILogger<PushNotificationService> logger
            )
        {
            _configuration = configuration;

            _mediator = mediator;
            //_logger = logger;
        }
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;
        //private readonly ILogger<PushNotificationService> _logger;
        public async Task<bool> AttendanceProcess(Guid? CompanyId, Guid? EmployeeId, DateTime? StartDate, DateTime? EndDate)
        {
            var processingCommand = new Commands.V1.AttendanceProcessDataCommand
            {
                CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                EmployeeId = (EmployeeId != null && EmployeeId != Guid.Empty) ? EmployeeId.Value : Guid.Empty,
                ProcessingStartDate = StartDate.HasValue ? StartDate.Value.Date : DateTime.Now.Date,
                ProcessingEndDate = EndDate.HasValue ? EndDate.Value.Date : DateTime.Now.Date
            };
            await _mediator.Send(processingCommand);
            return true;
        }
    }
}
