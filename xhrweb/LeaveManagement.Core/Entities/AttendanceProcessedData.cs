using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Entities
{
    public class AttendanceProcessedData : BaseEntity<Guid>
    {
        public Guid? EmployeeId { get; private set; }
        public DateTime PunchDate { get; private set; }
        public string PunchYear { get; private set; }
        public Int32? PunchMonth { get; private set; }
        public string Status { get; set; }
        public Guid? FinancialYearId { get; set; }
        public Guid? MonthCycleId { get; set; }
        public AttendanceProcessedData(Guid id) : base(id) { }
        private AttendanceProcessedData() : base(Guid.NewGuid()) { }
    }
}
