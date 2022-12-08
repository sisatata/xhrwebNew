using System;


namespace PayrollManagement.Application.IncomeTaxParameter .Commands
{
  public  class IncomeTaxParameterCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
