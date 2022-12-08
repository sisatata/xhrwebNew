using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.Employee.Queries.Models
{
    public class EmployeeVM
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? PositionId { get; set; }
        public string PositionName { get; set; }
        public string FullName { get; set; }
        public string FullNameLC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ReferenceNumber { get; set; }
        public Guid? NationalityId { get; set; }
        public string NationalityName { get; set; }
        public Guid? GenderId { get; set; }
        public string GenderName { get; set; }
        public Guid? GradeId { get; set; }
        public string GradeName { get; set; }
        public string EmployeeImageUri { get; set; }
        public string LoginId { get; set; }
        public int? DesignationOrder { get; set; }

    }
}




