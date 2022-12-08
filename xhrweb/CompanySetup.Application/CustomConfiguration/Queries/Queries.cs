using CompanySetup.Application.CustomConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.CustomConfiguration.Queries
{
    public static class Queries
    {
        public class GetCustomConfigurationList : IRequest<List<CustomConfigurationVM>>
        {
            public Guid CompanyId { get; set; }
        }
        public class GetCustomConfigurationListByCompany : IRequest<List<CustomConfigurationCompanyCodeVM>>
        {
            public Guid CompanyId { get; set; }
            public string Code { get; set; }
        }
        public class GetCustomConfigurationListByEmployee : IRequest<List<EmployeeCustomConfigurationVM>>
        {
            public Guid EmployeeId { get; set; }
            public string Code { get; set; }
        }

        public class GetCustomConfiguration : IRequest<CustomConfigurationVM>
        {
            public Guid Id { get; set; }
        }
    }
}
