using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities
{
    public class AttendanceCalendar : BaseEntity<Guid>
    {
        public string Year { get; set; }
        public DateTime YearStartDate { get; set; }
        public DateTime YearEndDate { get; set; }
        public Guid CompanyId { get; set; }
        public AttendanceCalendar(Guid id) : base(id) { }

        private AttendanceCalendar() : base(Guid.NewGuid()) { }
    }
}
