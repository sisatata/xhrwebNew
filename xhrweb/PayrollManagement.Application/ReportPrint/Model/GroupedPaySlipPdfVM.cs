using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class GroupedPaySlipPdfVM
    {
        public string CompanyName { get; set; }

        public string MonthCycleName { get; set; }
        public string Address { get; set; }
        public string FinancialYearName { get; set; }
        public List<ReportPaySlipPdfVM> DataList { get; set; }

        public string TotalNetPayableSalaryInWord { get; set; }
    }
}
