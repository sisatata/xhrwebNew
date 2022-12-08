using CompanySetup.Application.DefaultConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.DefaultConfiguration.Queries
{
    public static class Queries
    {
        public class GetDefaultConfigurationList : IRequest<List<DefaultConfigurationVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetDefaultConfiguration : IRequest<DefaultConfigurationVM>
        {
            public Guid Id { get; set; }
        }
    }
}
