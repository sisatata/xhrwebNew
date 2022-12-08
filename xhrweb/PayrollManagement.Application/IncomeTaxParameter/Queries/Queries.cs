using PayrollManagement.Application.IncomeTaxParameter.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.IncomeTaxParameter.Queries
{
    public static class Queries
    {
        public class GetIncomeTaxParameterList : IRequest<List<IncomeTaxParameterVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetIncomeTaxParameter : IRequest<IncomeTaxParameterVM>
        {
            public Guid Id { get; set; }
        }
    }
}
