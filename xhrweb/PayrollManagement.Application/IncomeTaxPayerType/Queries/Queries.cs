using PayrollManagement.Application.IncomeTaxPayerType.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.IncomeTaxPayerType.Queries
{
    public static class Queries
    {
        public class GetIncomeTaxPayerTypeList : IRequest<List<IncomeTaxPayerTypeVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetIncomeTaxPayerType : IRequest<IncomeTaxPayerTypeVM>
        {
            public Guid Id { get; set; }
        }
    }
}
