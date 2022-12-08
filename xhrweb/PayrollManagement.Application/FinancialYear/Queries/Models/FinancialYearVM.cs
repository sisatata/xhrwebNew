using PayrollManagement.Application.MonthCycle.Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.FinancialYear.Queries.Models
{
   public  class FinancialYearVM
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string FinancialYearName { get;  set; }
        public string FinancialYearLocalizedName { get;  set; }
        public DateTime? FinancialYearStartDate { get;  set; }
        public DateTime? FinancialYearEndDate { get;  set; }
        public bool IsRunningYear { get;  set; }
        public short SortOrder { get;  set; }
        public bool IsDeleted { get;  set; }

        public List<MonthCycleVM> MonthCycleList { get; set; }
    }
}
