using CompanySetup.Application.ConfirmationRule.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.ConfirmationRule.Queries
{
    public static class Queries
    {
        public class GetConfirmationRuleList : IRequest<List<ConfirmationRuleVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetConfirmationRule : IRequest<ConfirmationRuleVM>
        {
            public Guid Id { get; set; }
        }

        public class GetConfirmationRuleActiveList : IRequest<List<ConfirmationRuleVM>>
        {
            public Guid CompanyId { get; set; }
        }
    }
}
