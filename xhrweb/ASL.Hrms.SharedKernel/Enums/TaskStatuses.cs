using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
   public enum TaskStatuses
    {
        [Description("In Progress")]
        InProgress = 0,
        [Description("In Queue")]
        InQueue = 1,
        [Description("Done")]
        Done  = 3,
       

    }
}
