using EmployeeEnrollment.Application.EmployeeAddress.Queries.Models;
using EmployeeEnrollment.Application.EmployeeEmail.Queries.Models;
using EmployeeEnrollment.Application.EmployeeManager.Queries.Models;
using EmployeeEnrollment.Application.EmployeePhone.Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models
{
   public  class EmployeeProfileVM
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string FullNameLC { get; set; }
        public string DateOfBirth { get; set; }
        public string JoiningDate { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string GenderName { get; set; }
        public string EmployeeImageUri { get; set; }

        public List<EmployeeAddressVM> EmployeeAddressList { get; set; }
        public List<EmployeeManagerVM> EmployeeManagerList { get; set; }
        public List<EmployeeEmailVM> EmployeeEmailList { get; set; }
        public List<EmployeePhoneVM> EmployeePhoneList { get; set; }
    }
}
