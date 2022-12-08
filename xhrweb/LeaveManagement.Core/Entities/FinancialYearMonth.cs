using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Entities
{
    public class FinancialYearMonth : BaseEntity<Guid>
    {
        public int MonthNumber { get; set; }
        public int YearNumber { get; set; }
        public Guid FinancialYearId { get; set; }
        public string MonthCycleName { get; set; }
        public string MonthCycleLocalizedName { get; set; }
        public DateTime MonthStartDate { get; set; }
        public DateTime MonthEndDate { get; set; }
        public double TotalWorkingDays { get; set; }
        public Boolean RunningFlag { get; set; }
        public Boolean IsSelected { get; set; }
        public int? MonthSortOrder { get; set; }
        public Guid CompanyId { get; set; }
        public string FinancialYearName { get; set; }
        public string FinancialYearLocalizedName { get; set; }
        public DateTime FinancialYearStartDate { get; set; }
        public DateTime FinancialYearEndDate { get; set; }
        public Boolean IsRunningYear { get; set; }
        public int? YearSortOrder { get; set; }
        public FinancialYearMonth(Guid id) : base(id) { }
        private FinancialYearMonth() : base(Guid.NewGuid()) { }
    }
}
