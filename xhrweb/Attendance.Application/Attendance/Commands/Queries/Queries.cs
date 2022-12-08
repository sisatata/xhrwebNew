using Attendance.Application.Attendance.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Attendance.Application.Attendance.Queries
{
    public static class Queries
    {
        public class GetAttendanceList : IRequest<List<AttendanceVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetAttendance : IRequest<AttendanceVM>
        {
            public Guid Id { get; set; }
        }

        public class GetAllClockInOutListByEmployeeAndDate : IRequest<List<ClockInOutVM>>
        {
            public Guid EmployeeId { get; set; }
            public DateTime RequestDate{ get; set; }
        }
    }
}
