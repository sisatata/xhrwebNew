using System;


namespace PayrollManagement.Application.BenefitBillClaim.Commands
{
    public class BenefitBillClaimCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
