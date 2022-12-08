using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum ApprovalStatuses
    {
        [Description("Pending")]
        Pending = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Approved")]
        Approved = 3,
        [Description("Declined")]
        Declined = 9,
       [Description("All")]
        All = 10

    }
}
