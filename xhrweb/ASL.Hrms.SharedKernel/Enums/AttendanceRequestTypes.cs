using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum AttendanceRequestTypes
    {
        [Description("Late In")]
        LateIn = 1,
        [Description("Early Out")]
        EarlyOut = 2,
        [Description("Out Of Office")]
        OutOfOffice = 3,
        [Description("Clock In Request")]
        ForgotInTimePunch = 4,
        [Description("Clock Out Request")]
        ForgotOutTimePunch = 5
    }
}
