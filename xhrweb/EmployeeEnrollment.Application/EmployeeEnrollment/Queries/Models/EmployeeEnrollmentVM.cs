using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Queries.Models
{
    public class EmployeeEnrollmentVM
    {
        public Guid? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? QuitDate { get; set; }
        public Int16 StatusId { get; set; }
        public string EmployeeStatusName { get; set; }
        public DateTime? PermanentDate { get; set; }
        public Guid? JobTypeId { get; set; }
        public Guid? GradeId { get; set; }
        public Guid? SubGradeId { get; set; }
        public Guid? EmployeeTypeId { get; set; }
        public Guid? EmployeeCategoryId { get; set; }
        public Guid? ConfirmationId { get; set; }
        public Guid? GenderId { get; set; }
        public string EmployeeImageUri { get; set; }
        public string SignatureUri { get; set; }
        public int LeaveTypeGroupId { get; set; }
        public string LeaveTypeGroupName { get; set; }
    }
}
