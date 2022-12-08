using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LeaveManagement.Application.ReportPrint.Model
{
   public class ReportLeaveApplicationPdfVM
    {
        private string _approvalStatus;
        private DateTime _joiningDate;
        public string JoiningDateString { get; set; }
     
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }

        public Guid? Id { get; set; }
        public int? Balance { get; set; } 
        public string DepartmentName { get; set; }
        public string LeaveCalendar { get; set; }

        public DateTime? LAStartDate { get; set; }
        public string DesignationName { get; set; }
       // public string CompanyAddress { get; set; }


        public string  CompanyName { get; set; } 
        public string EmployeeId { get; set; } 

       // public string JoiningDate { get; set; }
        public DateTime JoiningDate 
        {
            get { return this._joiningDate; }
            set
            {
                this._joiningDate = value;
                JoiningDateString = _joiningDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }

        public string FullName { get; set; } 
        public string LeaveTypeName { get; set; } 
      
      

        public double? RemainingBalance { get; set; }
        public string StartDate { get; set; } 
        public string EndDate { get; set; } 
        public DateTime ApplyDate { get; set; }
        public int? DesignationOrder { get; set; }

        public string ApprovalStatusText { get; set; }
        public string ApprovalStatus 
        {
            get { return this._approvalStatus; }
            set
            {
                this._approvalStatus = value;
                ApprovalStatusText = ((ApprovalStatuses)Convert.ToInt32(!string.IsNullOrEmpty(this._approvalStatus) ? this._approvalStatus : "1")).ToString();
            }
        }

        public string Notes { get; set; } 
        public double LeaveDays { get; set; } 
        public string Reason { get; set; } 

        public string AddressOnLeave { get; set; }  
        public string EmergencyContact { get; set; } 
        public Boolean IsHalfDayLeave { get; set; }
        public int? HalfDayLeaveTypeId { get; set; } 
    }
}
