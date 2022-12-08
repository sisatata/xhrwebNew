using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.SalaryRuleDetail.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateSalaryRuleDetail : IRequest<SalaryRuleDetailCommandVM>
            {
                public Guid? SalaryRuleId { get; set; }
                public string SalaryHead { get; set; }
                public decimal? Value { get; set; }
                public string ValueType { get; set; }
                public string PercentDependOn { get; set; }
            }

            public class UpdateSalaryRuleDetail : IRequest<SalaryRuleDetailCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? SalaryRuleId { get; set; }
                public string SalaryHead { get; set; }
                public decimal? Value { get; set; }
                public string ValueType { get; set; }
                public string PercentDependOn { get; set; }
            }

            public class MarkAsDeleteSalaryRuleDetail : IRequest<SalaryRuleDetailCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
