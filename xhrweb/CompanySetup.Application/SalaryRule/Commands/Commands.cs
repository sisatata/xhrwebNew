using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.SalaryRule.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateSalaryRule : IRequest<SalaryRuleCommandVM>
            {
                public string RuleName { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateSalaryRule : IRequest<SalaryRuleCommandVM>
            {
                public Guid? Id { get; set; }
                public string RuleName { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteSalaryRule : IRequest<SalaryRuleCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
