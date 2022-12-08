using PayrollManagement.Application.IncomeTaxInvestment.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.IncomeTaxInvestment.Queries
{
    public static class Queries
    {
        public class GetIncomeTaxInvestmentList : IRequest<List<IncomeTaxInvestmentVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetIncomeTaxInvestment : IRequest<IncomeTaxInvestmentVM>
        {
            public Guid Id { get; set; }
        }
    }
}
