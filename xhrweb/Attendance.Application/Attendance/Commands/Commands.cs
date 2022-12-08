using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Attendance.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateAttendance : IRequest<AttendanceCommandVM>
            {
                public Guid? CardId { get; set; }
                public Guid? EmployeeId { get; set; }
                public string AttendanceYear { get; set; }
                public DateTime? AttendanceDate { get; set; }
                public Int16? Punchtype { get; set; }
                public Boolean OverNightMark { get; set; }
                public decimal? Latitude { get; set; }
                public decimal? Longitude { get; set; }

                public ushort ClockTimeType { get; set; }
                public string Remarks { get; set; }
                public string ClockTimeAddress { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class CreateAttendanceDevice : IRequest<AttendanceCommandVM>
            {
                public string CardMaskId { get; set; }
                public string CompanyMaskId { get; set; }
                public string AttendanceYear { get; set; }
                public DateTime? AttendanceDate { get; set; }
                public Int16? Punchtype { get; set; }
                //public Boolean OverNightMark { get; set; }
                //public decimal? Latitude { get; set; }
                //public decimal? Longitude { get; set; }

                public ushort ClockTimeType { get; set; }
                public string Remarks { get; set; }
                public string ClockTimeAddress { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class CreateAttendanceDeviceBulk : IRequest<AttendanceCommandVM>
            {
                public List<CreateAttendanceDevice> CreateAttendanceDeviceList { get; set; }
            }

            public class CreateAttendanceBulk : IRequest<AttendanceCommandVM>
            {
                public List<CreateAttendance> CreateAttendanceList { get; set; }
            }
            public class UpdateAttendance : IRequest<AttendanceCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? CardId { get; set; }
                public Guid? EmployeeId { get; set; }
                public string AttendanceYear { get; set; }
                public DateTime? AttendanceDate { get; set; }
                public Int16? Punchtype { get; set; }
                public Boolean OverNightMark { get; set; }
                public decimal? Latitude { get; set; }
                public decimal? Longitude { get; set; }
                public Boolean IsDeleted { get; set; }

                public ushort ClockTimeType { get; set; }
                public string Remarks { get; set; }
                public string ClockTimeAddress { get; set; }
            }

            public class MarkAsDeleteAttendance : IRequest<AttendanceCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
