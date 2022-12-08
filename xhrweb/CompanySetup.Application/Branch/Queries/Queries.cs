using CompanySetup.Application.Branch.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.Branch.Queries
{
    public static class Queries
    {
        public class GetBranchByCompany : IRequest<List<BranchVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetBranchById : IRequest<BranchVM>
        {
            public Guid BranchId { get; set; }
        }
    }
}
