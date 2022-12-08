using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateAttendanceProcessedData : IRequest<AttendanceProcessedDataCommandVM>
            {
                public Guid? EmployeeId { get; set; }
                public DateTime PunchDate { get; set; }
                public string PunchYear { get; set; }
                public Int32? PunchMonth { get; set; }
                public DateTime? TimeIn { get; set; }
                public DateTime? TimeOut { get; set; }
                public DateTime? ShiftIn { get; set; }
                public DateTime? ShiftOut { get; set; }
                public DateTime? BreakIn { get; set; }
                public DateTime? BreakOut { get; set; }
                public DateTime? BreakLate { get; set; }
                public DateTime? Late { get; set; }
                public Guid? ShiftId { get; set; }
                public string ShiftCode { get; set; }
                public DateTime? RegularHour { get; set; }
                public DateTime? OTHour { get; set; }
                public string Status { get; set; }
                public string Status_V2 { get; set; }
                public DateTime? BuyerShiftIn { get; set; }
                public DateTime? BuyerShiftOut { get; set; }
                public DateTime? BuyerOTTime { get; set; }
                public string Remarks { get; set; }
                public Boolean IsLunchOut { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
            }

            public class UpdateAttendanceProcessedData : IRequest<AttendanceProcessedDataCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public DateTime PunchDate { get; set; }
                public string PunchYear { get; set; }
                public Int32? PunchMonth { get; set; }
                public DateTime? TimeIn { get; set; }
                public DateTime? TimeOut { get; set; }
                public DateTime? ShiftIn { get; set; }
                public DateTime? ShiftOut { get; set; }
                public DateTime? BreakIn { get; set; }
                public DateTime? BreakOut { get; set; }
                public DateTime? BreakLate { get; set; }  
                public DateTime? Late { get; set; }
                public Guid? ShiftId { get; set; }
                public string ShiftCode { get; set; }
                public DateTime? RegularHour { get; set; }
                public DateTime? OTHour { get; set; }
                public string Status { get; set; }
                public string Status_V2 { get; set; }
                public DateTime? BuyerShiftIn { get; set; }
                public DateTime? BuyerShiftOut { get; set; }
                public DateTime? BuyerOTTime { get; set; }
                public string Remarks { get; set; }
                public Boolean IsLunchOut { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
            }

            public class MarkAsDeleteAttendanceProcessedData : IRequest<AttendanceProcessedDataCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class AttendanceProcessDataCommand : IRequest<AttendanceProcessedCommandVM>
            {
                public Guid CompanyId { get; set; }
                public Guid AttendanceCalendarId { get; set; }
                public Guid? EmployeeId { get; set; }
                public DateTime ProcessingStartDate { get; set; }
                public DateTime ProcessingEndDate { get; set; }
            }

            public class AttendanceProcessDataHangfireBackgroundCommand : IRequest<List<AttendanceProcessedHangfireBackgroundCommandVM>>
            {
                public Guid AttendanceCalendarId { get; set; }
                public DateTime ProcessingStartDate { get; set; }
                public DateTime ProcessingEndDate { get; set; }
            }
        }
    }
}
