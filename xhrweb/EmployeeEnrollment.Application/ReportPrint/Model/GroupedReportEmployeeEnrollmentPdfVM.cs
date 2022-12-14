using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.ReportPrint.Model
{
   public class GroupedReportEmployeeEnrollmentPdfVM
    {
        public string CompanyName { get; set; }
        public string CompanyLogoUri { get; set; }
        public string Address { get; set; }
        public string ReportTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ReportEmployeeEnrollmentPdfVM> DataList { get; set; }
    }
}
