using System;


namespace PayrollManagement.Application.IncomeTaxInvestment .Commands
{
  public  class IncomeTaxInvestmentCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
