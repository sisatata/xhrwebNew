using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeLeaveEncashment.Queries.Models
{
    public class EmployeeLeaveEncashmentVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public DateTime? EncashDate { get; set; }
        public Guid? FinancialYearId { get; set; }
        public Guid? MonthCycleId { get; set; }
        public decimal? ELEncashedDays { get; set; }
        public decimal? EncashedAmount { get; set; }
        public string Remarks { get; set; }
        public string LeaveTypeName { get; set; }
        public string FullName { get; set; }
        public string FinancialYearName { get; set; }
        public string MonthCycleName { get; set; }
    }
}
