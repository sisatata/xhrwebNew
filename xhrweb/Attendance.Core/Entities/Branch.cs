using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
   public class Branch : BaseEntity<Guid>
    {
        public Guid CompanyId { get; private set; }
        public string BranchName { get; private set; }
        public string ShortName { get; private set; }
        public string BranchLocalizedName { get; private set; }
        public uint SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public Branch(Guid id) : base(id) { }
        private Branch() : base(Guid.NewGuid()) { }
    }
}
