using System;


namespace PayrollManagement.Application.IncomeTaxPayerType .Commands
{
  public  class IncomeTaxPayerTypeCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
