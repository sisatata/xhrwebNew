using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum ClockTypes
    {
        [Description("In Time")]
        InTime = 1,
        [Description("Out Time")]
        OutTime = 2,
        [Description("Check In")]
        CheckIn = 3,
        [Description("Punch")]
        Punch = 0
    }
}
