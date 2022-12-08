using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
   public class GroupedBonusSheetPdfVM
    {
        public string CompanyName { get; set; }
        public string LookUpTypeName { get; set; }
        public string CompanyLogoUri { get; set; }
        public string Address { get; set; }
        public string FinancialYearName { get; set; }
        public string TotalNetPayableBonusInWord { get; set; }
        public List<ReportBonusSheetPdfVM> DataList { get; set; }

       
    }

}

