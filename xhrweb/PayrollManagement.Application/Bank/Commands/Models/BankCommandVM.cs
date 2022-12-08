using System;


namespace PayrollManagement.Application.Bank.Commands
{
    public class BankCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
