using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Attendance.Application.AttendanceProcessedData.Queries.Models
{
    public class ReportAttendanceProcessedDataPdfVM
    {
        private DateTime _date;
        private DateTime _joiningDate;

        public string CompanyLogoUri { get; set; }
        public string EmployeeId { get; set; }
        public string TotalOT { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? EID { get; set; }

        public string FullName { get; set; }
        public string Designation { get; set; }
        public DateTime OTHour { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string ReportTitle { get; set; }

        public string JoiningDateString { get; set; }
        public DateTime JoiningDate {
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
        public int? DesignationOrder { get; set; }
        public DateTime Late { get; set; }
        public string ShiftCode { get; set; }
        //public string RegularHour { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
