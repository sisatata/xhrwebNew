using System;


namespace PayrollManagement.Application.EmployeeBankAccount.Commands
{
    public class EmployeeBankAccountCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
