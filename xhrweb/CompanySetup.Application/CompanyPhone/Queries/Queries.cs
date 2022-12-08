using CompanySetup.Application.CompanyPhone.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.CompanyPhone.Queries
{
    public static class Queries
    {
        public class GetCompanyPhoneList : IRequest<List<CompanyPhoneVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetCompanyPhone : IRequest<CompanyPhoneVM>
        {
            public Guid Id { get; set; }
        }
    }
}
