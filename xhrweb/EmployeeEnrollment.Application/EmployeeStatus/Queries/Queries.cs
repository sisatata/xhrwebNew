using EmployeeEnrollment.Application.EmployeeStatus.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeStatus.Queries
{
    public static class Queries
    {
        public class GetEmployeeStatusList : IRequest<List<EmployeeStatusVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeStatus : IRequest<EmployeeStatusVM>
        {
            public Guid Id { get; set; }
        }
    }
}
