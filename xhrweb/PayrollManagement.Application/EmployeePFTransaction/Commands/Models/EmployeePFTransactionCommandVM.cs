using System;


namespace PayrollManagement.Application.EmployeePFTransaction.Commands
{
    public class EmployeePFTransactionCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
