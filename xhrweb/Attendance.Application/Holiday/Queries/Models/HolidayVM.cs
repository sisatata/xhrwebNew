using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Holiday.Queries.Models
{
    public class HolidayVM
    {
        public Guid? Id { get; set; }
        public DateTime HolidayDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string ReasonLocalized { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
