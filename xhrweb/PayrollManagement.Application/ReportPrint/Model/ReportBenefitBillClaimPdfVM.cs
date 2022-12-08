using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
    public class ReportBenefitBillClaimPdfVM
    {
        public string EmployeeId { get; set; }
        public string ApplicantName { get; set; }
        public string BillType { get; set; }

        public string ClaimDate { get; set; }
        public decimal? AllocatedAmount { get; set; }
        public decimal? ClaimAmount { get; set; }
        public string Remarks { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovedDate { get; set; }
        public string Note { get; set; }
        public string ManagerName { get; set; }

        private DateTime _date;
        private DateTime _billDate;
        private DateTime _joiningDate;
       
        public string Designation { get; set; }

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string ReportTitle { get; set; }

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
        public string Department { get; set; }
        public string Branch { get; set; }
        public string DateString
        {
            get; set;
        }

        public DateTime BillDate
        {
            get { return this._billDate; }
            set
            {
                this._billDate = value;
                DateString = _billDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
        //public DateTime Date
        //{
        //    get { return this._date; }
        //    set
        //    {
        //        this._date = value;
        //        DateString = _date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    }
        //}

        public string LocationFrom { get; set; }
        public int? DesignationOrder { get; set; }
        public string LocationTo { get; set; }
        public string VehicleTypeId { get; set; }
        public int? NumberOfAttendees { get; set; }
        public string NameOfAttendees { get; set; }
        public string EmployeeSignature { get; set; }
        public string ManagerSignature { get; set; }


    }
}
