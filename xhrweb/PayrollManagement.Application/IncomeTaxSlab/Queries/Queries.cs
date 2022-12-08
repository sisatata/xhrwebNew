using PayrollManagement.Application.IncomeTaxSlab.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.IncomeTaxSlab.Queries
{
    public static class Queries
    {
        public class GetIncomeTaxSlabList : IRequest<List<IncomeTaxSlabVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetIncomeTaxSlab : IRequest<IncomeTaxSlabVM>
        {
            public Guid Id { get; set; }
        }
    }
}
