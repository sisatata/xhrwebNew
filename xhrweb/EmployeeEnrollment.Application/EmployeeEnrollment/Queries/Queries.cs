using EmployeeEnrollment.Application.EmployeeEnrollment.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Queries
{
    public static class Queries
    {
        public class GetEmployeeEnrollmentList : IRequest<List<EmployeeEnrollmentVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeEnrollment : IRequest<EmployeeEnrollmentVM>
        {
            public Guid EmployeeId { get; set; }
        }
    }
}
