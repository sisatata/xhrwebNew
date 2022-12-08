using System;


namespace PayrollManagement.Application.BenefitDeductionConfig.Commands
{
    public class BenefitDeductionConfigCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
