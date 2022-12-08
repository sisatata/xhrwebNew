using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
   public class Designation : BaseEntity<Guid>
    {
        public Guid LinkedEntityId { get; private set; }
        public string LinkedEntityType { get; private set; }
        public string DesignationName { get; private set; }
        public string DesignationLocalizedName { get; private set; }
        public string ShortName { get; private set; }
        public uint SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public Designation(Guid id) : base(id) { }
        private Designation() : base(Guid.NewGuid()) { }
    }
}
