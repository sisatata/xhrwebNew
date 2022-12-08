using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.Models
{
    public class Employee : BaseEntity<Guid>
    {
        public string DisplayName { get; set; }
        public Guid LoginId { get; set; }
        public Guid  CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeImageUri { get; set; }
        public string DesignationName { get; set; }
        public Employee(Guid id) : base(id) { }
        private Employee() : base(Guid.NewGuid()) { }
    }
}
