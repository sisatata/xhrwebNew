using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class GroupedProvidentFundReportPdfVM
    {
        public string CompanyName { get; set; }
        public string MonthCycleName { get; set; }
        public string CompanyLogoUri { get; set; }
        public string Address { get; set; }
        public string FinancialYearName { get; set; }
        public List<ReportProvidentFundPdfVM> DataList { get; set; }

        public string TotalNetPayableSalaryInWord { get; set; }
    }
}

