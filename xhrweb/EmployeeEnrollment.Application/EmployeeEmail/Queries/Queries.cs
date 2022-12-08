using EmployeeEnrollment.Application.EmployeeEmail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeEmail.Queries
{
    public static class Queries
    {
        public class GetEmployeeEmailListByEmployeeId : IRequest<List<EmployeeEmailVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeEmailByID : IRequest<EmployeeEmailVM>
        {
            public Guid Id { get; set; }
        }
    }
}
