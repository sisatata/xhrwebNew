using System;


namespace PayrollManagement.Application.BenefitDeductionCode.Commands
{
    public class BenefitDeductionCodeCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
