using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class ReportPaySlipPdfVM
    {
		public string EmployeeId { get; set; }
		public  Guid? SalaryProcessedId { get; set; }
		public string FullName { get; set; }
		public string TotalNetPayableSalaryInWord { get; set; }
		public string AbsentDeduction { get; set; }
		public string OtherDeduction { get; set; }
		public string MonthCycleName { get; set; }
		public string FinancialYearName { get; set; }
		public string CompanyName { get; set; }
		private DateTime _joiningDate;
		public string JoiningDateString { get; set; }
		public decimal? Basic { get; set; }

		public decimal? HouseRent { get; set; }
		public decimal? Medical { get; set; }
		public decimal? Conveyance { get; set; }
		public string PaymentOption { get; set; }
		public DateTime JoiningDate
		{
			get { return this._joiningDate; }
			set
			{
				this._joiningDate = value;
				JoiningDateString = _joiningDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
			}
		}

		public string DesignationName { get; set; }
		public int? DesignationOrder { get; set; }
		public string GradeName { get; set; }
		public short? TotalDaysInMonth { get; set; }
		public short? TotalWorkingDays { get; set; }
		public string OTHour { get; set; }
		public short? OTRate { get; set; }
		public short? TotalPresentDays { get; set; }
		public short? TotalAbsentDays { get; set; }
		public decimal? Food { get; set; }
		public short? TotalLeaveDays { get; set; }
		public short? WeeklyOffDays { get; set; }
		public short? TotalWorkingHoliday { get; set; }
		public string Address { get; set; }

		public string DepartmentName { get; set; }
		public decimal? GrossSalary { get; set; }
		public decimal? PayableSalary { get; set; }
		public decimal? TotalDeductionAmount { get; set; }
		public decimal? NetPayableAmount { get; set; }

		public Guid SalaryId { get; set; }

		public string Remarks { get; set; }



	}
}
