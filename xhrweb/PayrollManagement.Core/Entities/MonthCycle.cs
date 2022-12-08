using ASL.Hrms.SharedKernel;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities
{
   public class MonthCycle : BaseEntity<Guid>
	{
		public Guid CompanyId { get; private set; }
		public Guid FinancialYearId { get; private set; }
		public string MonthCycleName { get; private set; }
		public string MonthCycleLocalizedName { get; private set; }
		public DateTime MonthStartDate { get; private set; }
		public DateTime MonthEndDate { get; private set; }
		public double TotalWorkingDays { get; private set; }
		public bool RunningFlag { get; private set; }
		public bool IsSelected { get; private set; }
		public int SortOrder { get; private set; }

		public bool IsDeleted { get; private set; }

		


		public MonthCycle(Guid id) : base(id) { }
		private MonthCycle() : base(Guid.NewGuid()) { }

	}
}
