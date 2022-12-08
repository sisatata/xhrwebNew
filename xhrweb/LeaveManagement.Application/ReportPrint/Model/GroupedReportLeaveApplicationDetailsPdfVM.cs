using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.ReportPrint.Model
{
   public class GroupedReportLeaveApplicationDetailsPdfVM
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CompanyLogoUri { get; set; }
        public string ReportTitle { get; set; }
        public List<ReportLeaveApplicationDetailsPdfVM> DataList { get; set; }

    }
}
