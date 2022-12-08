using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeManager.Queries.Models
{
    public class EmployeeManagerVM
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid? ManagerId { get; set; }
        public string ManagerName { get; set; }

        //public string ManagerName { get; set; }
        public Boolean IsPrimaryManager { get; set; }
        public Guid? AssignedBy { get; set; }
        public DateTime? AssignDate { get; set; }
        public Guid? UnAssignedBy { get; set; }
        public DateTime? UnAssignDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ManagerTypeId { get; set; }
        public string ManagerTypeName { get; set; }
    }
}
