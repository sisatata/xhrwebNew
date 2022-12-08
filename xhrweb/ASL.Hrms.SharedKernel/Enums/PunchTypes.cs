using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum PunchTypes
    {
        [Description("Punch Machine")]
        PunchMachine = 1,
        [Description("Mobile")]
        Mobile = 2,
        [Description("Web")]
        Web = 3,
        [Description("Geo Fence")]
        GeoFence = 4,

        [Description("Manual Edit")]
        ManualEdit = 9
    }
}
