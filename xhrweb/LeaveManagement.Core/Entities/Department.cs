using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Entities
{
  public  class Department : BaseEntity<Guid>
    {
        public Guid CompanyId { get; private set; }
        public Guid BranchId { get; private set; }
        public string DepartmentName { get; private set; }
        public string ShortName { get; private set; }
        public string DepartmentLocalizedName { get; private set; }
        public uint SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }
        public Department(Guid id) : base(id) { }
        private Department() : base(Guid.NewGuid()) { }


    }
}
