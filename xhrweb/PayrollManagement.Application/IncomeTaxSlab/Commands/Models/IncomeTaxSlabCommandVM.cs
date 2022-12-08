using System;


namespace PayrollManagement.Application.IncomeTaxSlab .Commands
{
  public  class IncomeTaxSlabCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
