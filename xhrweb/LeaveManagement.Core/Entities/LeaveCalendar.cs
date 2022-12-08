using ASL.Hrms.SharedKernel;
using System;

namespace LeaveManagement.Core.Entities
{
    public class LeaveCalendar : BaseEntity<Guid>
    {
        public string Year { get; set; }
        public DateTime YearStartDate { get; set; }
        public DateTime YearEndDate { get; set; }
        public Guid CompanyId { get; set; }
        public LeaveCalendar(Guid id) : base(id) { }

        private LeaveCalendar(): base(Guid.NewGuid()) { }
    }
}
