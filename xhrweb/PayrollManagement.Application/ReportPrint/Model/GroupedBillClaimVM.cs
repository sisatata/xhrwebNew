using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.ReportPrint.Model
{
    public class GroupedBillClaimVM
    {
        public string ApplicantName { get; set; }
        public string ManagerName { get; set; }
        public DateTime BillType { get; set; }
        public DateTime BillDate { get; set; }
        public string CompanyAddress { get; set; }
        public string TotalAmountInWord { get; set; }

        public string EmployeeSignature { get; set; }
        public string ManagerSignature { get; set; }
        public string CompanyLogoUri { get; set; }


        public List<ReportBenefitBillClaimPdfVM> DataList { get; set; }
    }
}
