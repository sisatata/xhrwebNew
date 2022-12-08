using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.ReportPrint.Model
{
   public class GroupedAttendanceDetailsPdfVM
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ReportTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ReportAttendaceDetailsPdfVM> DataList { get; set; }

    }
}
