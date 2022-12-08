using System;
namespace CompanySetup.Application.ConfirmationRule.Commands
{
    public class ConfirmationRuleCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
