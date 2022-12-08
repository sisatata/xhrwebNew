using CompanySetup.Application.CompanyEmail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.CompanyEmail.Queries
{
    public static class Queries
    {
        public class GetCompanyEmailList : IRequest<List<CompanyEmailVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetCompanyEmail : IRequest<CompanyEmailVM>
        {
            public Guid Id { get; set; }
        }
    }
}
