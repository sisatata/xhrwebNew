using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.ReportPrint.Model
{
  public  class GroupLeaveApplicationPdfVM
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyLogoUri { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<ReportLeaveApplicationPdfVM> DataList { get; set; }
    }
}
