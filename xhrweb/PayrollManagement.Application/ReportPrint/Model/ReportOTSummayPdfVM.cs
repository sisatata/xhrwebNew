using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class ReportOTSummayPdfVM
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public decimal? Hours { get; set; }
        public decimal? Minutes { get; set; }
        public decimal? TotalOTHour { get; set; }
        public string TotalOTHourString { get; set; }
        public decimal? OTRate { get; set; }
        public decimal? TotalOTRate { get; set; }
        public string CompanyLogoUri { get; set; }

        public decimal? TotalOTAmount { get; set; }


    }
}
