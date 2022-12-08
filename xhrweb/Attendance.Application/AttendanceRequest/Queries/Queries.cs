using Attendance.Application.AttendanceRequest.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Attendance.Application.AttendanceRequest.Queries
{
    public static class Queries
    {
        public class GetAttendanceRequestList : IRequest<List<AttendanceRequestVM>>
        {
            public Guid CompanyId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class GetAttendanceBySearchRequestList : IRequest<List<AttendanceRequestVM>>
        {
            public Guid EmployeeId { get; set; }
            public int RequestTypeId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }

        }

        public class GetAttendanceRequest : IRequest<AttendanceRequestVM>
        {
            public Guid Id { get; set; }
        }

        public class GetPendingAttendanceReqNotificationByManager : IRequest<List<AttendanceRequestNotificationVM>>
        {
            public Guid ManagerId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
    }
}
