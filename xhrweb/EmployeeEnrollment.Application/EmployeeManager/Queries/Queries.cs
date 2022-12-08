using EmployeeEnrollment.Application.EmployeeManager.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeManager.Queries
{
    public static class Queries
    {
        public class GetEmployeeManagerList : IRequest<List<EmployeeManagerVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeManager : IRequest<EmployeeManagerVM>
        {
            public Guid Id { get; set; }
        }
    }
}
