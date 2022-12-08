using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class GroupedReportOTSummaryPdfVM
    {
        public string CompanyName { get; set; }
       public string NetPayableOT { get; set; }
        public  decimal?  Hours { get; set; }
        public double? OTRate { get; set; } = 2.0;
        public decimal? TotalOTRate { get; set; } 
        public   decimal? Minutes { get; set; }
        public string Address { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DepartmentName { get; set; }

        public string CompanyLogoUri { get; set; }
        


    }
}
