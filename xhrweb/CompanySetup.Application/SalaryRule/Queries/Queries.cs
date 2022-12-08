using CompanySetup.Application.SalaryRule.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.SalaryRule.Queries
{
    public static class Queries
    {
        public class GetSalaryRuleList : IRequest<List<SalaryRuleVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetSalaryRule : IRequest<SalaryRuleVM>
        {
            public Guid Id { get; set; }
        }

        public class GetSalaryRuleActiveList : IRequest<List<SalaryRuleVM>>
        {
            public Guid CompanyId { get; set; }
        }
    }
}
