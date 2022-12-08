using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitBillClaim.Queries.Models
{
    public class BenefitBillClaimSummaryVM
    {
        public Guid BenefitDeductionId { get; set; }
        public string BenefitDeductionCode { get; set; }
        public string Name { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
    }
}
