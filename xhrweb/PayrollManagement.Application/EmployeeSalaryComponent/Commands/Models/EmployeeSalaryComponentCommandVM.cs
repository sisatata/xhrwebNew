using System;


namespace PayrollManagement.Application.EmployeeSalaryComponent.Commands
{
    public class EmployeeSalaryComponentCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
