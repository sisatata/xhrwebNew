using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Attendance.Application.ReportPrint.Model
{
   public class ReportAttendaceDetailsPdfVM
    {
        private DateTime _date;
        private DateTime _joiningDate;
        public string EmployeeId { get; set; }
        public string CompanyLogoUri { get; set; }
        public Guid? BranchId { get; set; }
        public string Branch { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }

        public int? DesignationOrder { get; set; }
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
      
        public string DateString
        {
            get; set;
        }
        public DateTime Date
        {
            get { return this._date; }
            set
            {
                this._date = value;
                DateString = _date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }

        //public string PunchYear { get; set; }
        //public Int32? PunchMonth { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime OfficeInTime { get; set; }
        public DateTime OfficeOutTime { get; set; }

        public DateTime Late { get; set; }
        public string ShiftCode { get; set; }
        //public string RegularHour { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
