using System;


namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent .Commands
{
  public  class EmployeeSalaryProcessedDataComponentCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
