using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class ReportLeaveEncashmentPdfVM
    {
        public Guid EmployeeId { get; set; }
        public string EID { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public decimal? EncashedAmount { get; set; }
        public string FullName { get; set; }
        public string FinancialYearName { get; set; }
        public string CompanyName { get; set; }
        private DateTime _joiningDate;
        public string JoiningDateString { get; set; }
       
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
        public decimal? GrossSalary { get; set; }

        public decimal? ELEncashedDays { get; set; }
        public string DepartmentName { get; set; }
        public Guid MonthCycleId { get; set; }
        public string MonthCycleName { get; set; }
        public string Remarks { get; set; }
    }
}
