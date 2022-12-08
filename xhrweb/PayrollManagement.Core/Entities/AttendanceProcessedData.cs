using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities
{
    public class AttendanceProcessedData : BaseEntity<Guid>
    {
        public Guid? EmployeeId { get; private set; }
        public DateTime PunchDate { get; private set; }
        
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? ShiftIn { get; set; }
        public DateTime? ShiftOut { get; set; }
        public DateTime? BreakIn { get; set; }
        public DateTime? BreakOut { get; set; }
        public DateTime? BreakLate { get; set; }
        public DateTime? Late { get; set; }
        public string ShiftCode { get; set; }
        public DateTime? RegularHour { get; set; }
        public DateTime? OTHour { get; set; }
        public string Status { get; set; }
        public Guid? FinancialYearId { get; set; }
        public Guid? MonthCycleId { get; set; }
        public Guid? CompanyId { get; set; }
        // not persisted
        public AttendanceProcessedData(Guid id) : base(id) { }
        private AttendanceProcessedData() : base(Guid.NewGuid()) { }

    }
}
