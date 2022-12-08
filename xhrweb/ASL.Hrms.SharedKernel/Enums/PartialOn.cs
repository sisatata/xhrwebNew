using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum PartialOn
    {
        [Description("Day Wise")]
        DayWise = 10,
        [Description("Month Wise")]
        MonthWise = 20,
        [Description("Year Wise")]
        YearWise = 30

    }
}
