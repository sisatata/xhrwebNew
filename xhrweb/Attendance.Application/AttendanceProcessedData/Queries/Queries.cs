using Attendance.Application.AttendanceProcessedData.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Attendance.Application.AttendanceProcessedData.Queries
{
    public static class Queries
    {
        public class GetAttendanceProcessedDataList : IRequest<List<AttendanceProcessedDataVM>>
        {
            public Guid CompanyId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
        public class GetAttendanceProcessedDataListByEmployeeAndDateRange : IRequest<AttendanceDataVM>
        {
            public Guid EmployeeId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
        public class GetAttendanceDataListByEmployeeAndDateRange : IRequest<List<AttendanceProcessedDataVM>>
        {
            public Guid EmployeeId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
        public class GetAttendanceProcessedData : IRequest<AttendanceProcessedDataVM>
        {
            public Guid Id { get; set; }
        }

        public class GetAttendanceProcessedReportData : IRequest<List<ReportAttendanceProcessedDataVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid EmployeeId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
        public class AttendanceMissPunchEmailSendToAdmin : IRequest<List<MissPunchAttendanceDataVM>>
        {
           
        }
        public class GetEmployeeAttendanceData : IRequest<List<AttendanceProcessedDataVM>>
        {
            public Guid CompanyId { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int PageNumber { get; set; } = 1;
            public string SearchText { get; set; }
            public int PageSize { get; set; } = 10;
            public bool GetAll { get; set; }
        }
    }
}
