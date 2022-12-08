using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
   public enum AttendanceReportTypes
    {
        [Description("Present")]
        Present = 1,
        [Description("Late Nn")]
        LateIn = 2,
        [Description("Absent")]
        Absent = 3,
        [Description("Early Out")]
        EarlyOut = 4,
        [Description("In Punch Miss")]
        InTimePunchMiss = 5,
        [Description("Out Punch Miss")]
        OutTimePunchMiss = 6
    }
}
