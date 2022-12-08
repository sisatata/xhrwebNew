using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.ConfirmationRule.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateConfirmationRule : IRequest<ConfirmationRuleCommandVM>
            {
                public string RuleName { get; set; }
                public string Description { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public Int16? ConfirmationType { get; set; }
                public Int16? ConfirmationMonths { get; set; }
                public Int16? SortOrder { get; set; }
                public Boolean IsActive { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateConfirmationRule : IRequest<ConfirmationRuleCommandVM>
            {
                public Guid? Id { get; set; }
                public string RuleName { get; set; }
                public string Description { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public Int16? ConfirmationType { get; set; }
                public Int16? ConfirmationMonths { get; set; }
                public Int16? SortOrder { get; set; }
                public Boolean IsActive { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteConfirmationRule : IRequest<ConfirmationRuleCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
