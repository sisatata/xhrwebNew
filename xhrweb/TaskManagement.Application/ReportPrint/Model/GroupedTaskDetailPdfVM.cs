using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.ReportPrint.Model
{
   public class GroupedTaskDetailPdfVM
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ReportTitle { get; set; }
        public string CompanyLogoUri { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ReportTaskDetailPdfVM> DataList { get; set; }
    }
}
