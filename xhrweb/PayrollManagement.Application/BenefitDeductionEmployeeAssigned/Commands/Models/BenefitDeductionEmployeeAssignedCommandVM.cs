using System;


namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned .Commands
{
  public  class BenefitDeductionEmployeeAssignedCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
