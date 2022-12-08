using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Entities
{
   public class LeaveType : BaseEntity<Guid>
    {
        public Guid CompanyId { get; private set; }
        public string LeaveTypeName { get; private set; }
        public string LeaveTypeShortName { get; private set; }
        public string LeaveTypeLocalizedName { get; private set; }
    }
}
