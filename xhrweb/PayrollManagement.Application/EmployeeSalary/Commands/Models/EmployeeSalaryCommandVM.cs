using System;


namespace PayrollManagement.Application.EmployeeSalary .Commands
{
  public  class EmployeeSalaryCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
