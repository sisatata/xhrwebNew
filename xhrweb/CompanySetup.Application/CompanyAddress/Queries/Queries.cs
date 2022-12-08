using CompanySetup.Application.CompanyAddress.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.CompanyAddress.Queries
{
    public static class Queries
    {
        public class GetCompanyAddressList : IRequest<List<CompanyAddressVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetCompanyAddress : IRequest<CompanyAddressVM>
        {
            public Guid Id { get; set; }
        }
    }
}
