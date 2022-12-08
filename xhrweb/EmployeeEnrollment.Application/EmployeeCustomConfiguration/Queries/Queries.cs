using EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries
{
    public static class Queries
    {
        public class GetEmployeeCustomConfigurationList : IRequest<List<EmployeeCustomConfigurationVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeCustomConfiguration : IRequest<EmployeeCustomConfigurationVM>
        {
            public Guid Id { get; set; }
        }
    }
}
