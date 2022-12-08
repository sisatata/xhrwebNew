using System;


namespace PayrollManagement.Application.BenefitDeductionInterval .Commands
{
  public  class BenefitDeductionIntervalCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
