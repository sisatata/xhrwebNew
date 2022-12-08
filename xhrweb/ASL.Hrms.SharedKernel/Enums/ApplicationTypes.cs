using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum NotificationTypes
    {
        [Description("Leave")]
        Leave = 1,
        [Description("Attendance")]
        Attendance = 2,
        [Description("Bill")]
        Bill = 3,
        [Description("Holiday Push Notification")]
        HolidayPush = 4,
        [Description("Notice Board Push Notification")]
        NoticeBoardPush = 5

    }
}
