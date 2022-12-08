using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class Employee : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public DateTime JoiningDate { get; set; }
        //public string Gender { get; set; }

        public Employee(Guid id) : base(id) { }
        private Employee() : base(Guid.NewGuid()) { }
    }
}
