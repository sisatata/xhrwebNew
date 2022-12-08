using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EmployeeEnrollment.Application.Chart.Model
{
   public class EmployeeConfirmationChartVM
    {
       // public string EmployeeId { get; set; }
        public string FullName { get; set; }

       // public string CompanyName { get; set; }
        private DateTime _joiningDate;
        private DateTime _resignDate;
        public DateTime _parmanentDate;
        public string ResignDateString { get; set; }
        public string JoiningDateString { get; set; }
        public string PermanentDateString { get; set; }
        public decimal? GrossSalary { get; set; }
      //  public int? StatusId { get; set; }
       // public string EmployeeImageUri { get; set; }
       // public string LookUpTypeName { get; set; }

        public DateTime JoiningDate
        {
            get { return this._joiningDate; }
            set
            {
                this._joiningDate = value;
                JoiningDateString = _joiningDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
        public DateTime ResignDate
        {
            get { return this._resignDate; }
            set
            {
                this._resignDate = value;
                ResignDateString = _resignDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
        public DateTime PermanentDate
        {
            get { return this._parmanentDate; }
            set
            {
                this._parmanentDate = value;
                PermanentDateString = _parmanentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }

        public string DesignationName { get; set; }
        public string GradeName { get; set; }

      //  public string Address { get; set; }

        public string DepartmentName { get; set; }


        

    }
}
