using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Attendance.Queries.Models
{
    public class AttendanceVM
    {
        public Guid? Id { get; set; }
        public Guid? CardId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string AttendanceYear { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public Int16? Punchtype { get; set; }
        public Boolean OverNightMark { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
