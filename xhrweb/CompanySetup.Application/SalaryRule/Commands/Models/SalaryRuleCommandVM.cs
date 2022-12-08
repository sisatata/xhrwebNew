using System;


namespace CompanySetup.Application.SalaryRule.Commands
{
    public class SalaryRuleCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
