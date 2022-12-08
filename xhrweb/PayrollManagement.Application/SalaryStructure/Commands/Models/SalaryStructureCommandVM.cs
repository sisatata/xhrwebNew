using System;


namespace PayrollManagement.Application.SalaryStructure.Commands
{
    public class SalaryStructureCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
