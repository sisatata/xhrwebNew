using CompanySetup.Application.SalaryRuleDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.SalaryRuleDetail.Queries
{
    public static class Queries
    {
        public class GetSalaryRuleDetailList : IRequest<List<SalaryRuleDetailVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetSalaryRuleDetail : IRequest<SalaryRuleDetailVM>
        {
            public Guid Id { get; set; }
        }
    }
}
