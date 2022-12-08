using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
namespace PayrollManagement.Application.ReportPrint.Model
{
    public class ReportBonusSheetPdfVM
    {
        public string EmployeeId { get; set; }
        public string LookUpTypeName { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }

        public string FullName { get; set; }
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
        public decimal? Food { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public decimal? PayableSalary { get; set; }
        public decimal? GrossSalary { get; set; }
        public decimal? NetPayableAmount { get; set; }
        public decimal? PayableBonus { get; set; }
        public string ServiceLength { get; set; }
        public string Remarks { get; set; }
    }
}
