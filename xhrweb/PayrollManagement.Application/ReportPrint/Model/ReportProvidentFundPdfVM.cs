using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class ReportProvidentFundPdfVM
    {
        public string CompanyEmployeeId { get; set; }

        public Guid EmployeeId { get; set; }
        public string MonthCycleName { get; set; }
         
        public Guid SalaryId { get; set; }
        public string FinancialYearName { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid PFYearId { get; set; }
        
        public Guid PFMonthId { get; set; }
        public string FullName { get; set; }
      
        public string CompanyName { get; set; }
        private DateTime _joiningDate;
        public string JoiningDateString { get; set; }
        public decimal? Basic { get; set; }
        public decimal? CompanyContribution { get; set; }
        public decimal? EmployeeContribution { get; set; }

        public decimal? TotalContribution { get; set; }

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
        public int? SortOrder { get; set; }
      
      
        public string Address { get; set; }
        public string DepartmentName { get; set; }
      
        public decimal? GrossSalary { get; set; }
       
       
        public string ServiceLength { get; set; }
        public string Remarks { get; set; }
    }
}
