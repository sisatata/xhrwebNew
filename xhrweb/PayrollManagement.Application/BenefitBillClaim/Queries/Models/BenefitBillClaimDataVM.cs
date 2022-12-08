using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitBillClaim.Queries.Models
{
   public class BenefitBillClaimDataVM
    {
        public List<BenefitBillClaimSummaryVM> BenefitBillClaimSummaries { get; set; }
        public List<BenefitBillClaimVM> BenefitBillClaims { get; set; }
    }
}
