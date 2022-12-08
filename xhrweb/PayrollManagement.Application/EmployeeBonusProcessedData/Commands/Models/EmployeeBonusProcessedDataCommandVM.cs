using System;


namespace PayrollManagement.Application.EmployeeBonusProcessedData .Commands
{
  public  class EmployeeBonusProcessedDataCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
