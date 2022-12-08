using System;


namespace PayrollManagement.Application.EmployeePaidIncomeTax.Commands
{
    public class EmployeePaidIncomeTaxCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
