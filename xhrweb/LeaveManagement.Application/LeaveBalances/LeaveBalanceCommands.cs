using LeaveManagement.Application.LeaveBalances.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveBalances
{
    public class LeaveBalanceCommands
    {
        public static class V1
        {
            public class ProcessLeaveBalanceCommand: IRequest<LeaveBalanceVM>
            {
                public Guid CompanyId { get; set; }
                public Guid LeaveCalendarId { get; set; }
                public Guid? EmployeeId { get; set; }
            }

            public class AdjustLeaveBalanceAfterEncashmentCommand : IRequest<LeaveBalanceVM>
            {
                public Guid CompanyId { get; set; }
                public Guid LeaveTypeId { get; set; }
                public Guid? EmployeeId { get; set; }
                public string LeaveCalendar { get; set; }
                public decimal? LeaveEncashedDays { get; set; }
            }
        }
    }
}
