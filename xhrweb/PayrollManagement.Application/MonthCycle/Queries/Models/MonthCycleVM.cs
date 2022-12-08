using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.MonthCycle.Queries.Models
{
   public  class MonthCycleVM
    {
		public Guid Id { get;  set; }
		public Guid CompanyId { get; set; }
		public string CompanyName { set; get; }
		public Guid FinancialYearId { get;  set; }
		public string FinancialYearName { get; set; }
		public string MonthCycleName { get;  set; }
		public string MonthCycleLocalizedName { get;  set; }
		public DateTime MonthStartDate { get;  set; }
		public DateTime MonthEndDate { get;  set; }
		public double TotalWorkingDays { get;  set; }
		public bool RunningFlag { get;  set; }		
		public bool IsSelected { get;  set; }
		public int SortOrder { get;  set; }
		public bool IsDeleted { get;  set; }
	}
}
