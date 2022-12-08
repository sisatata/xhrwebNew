using System;


namespace PayrollManagement.Application.SalaryStructureComponent .Commands
{
  public  class SalaryStructureComponentCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
