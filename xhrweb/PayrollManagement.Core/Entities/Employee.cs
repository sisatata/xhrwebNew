using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities
{
   public class Employee : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public DateTime JoiningDate { get; set; }
        public Guid? BranchId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public Guid PositionId { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public Guid GenderId { get; private set; }
        public Int16 StatusId { get; private set; }
        public DateTime? QuitDate { get; private set; }
        public DateTime? PermanentDate { get; private set; }
        public Guid CompanyId { get; set; }

        public Employee(Guid id) : base(id) { }
        private Employee() : base(Guid.NewGuid()) { }
    }
}