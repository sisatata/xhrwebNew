using EmployeeEnrollment.Application.EmployeeEducation .Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeEducation .Queries
{
    public static class Queries
    {
        public class GetEmployeeEducationList : IRequest< List< EmployeeEducationVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeEducation : IRequest< EmployeeEducationVM>
        {
            public Guid Id { get; set; }
        }
    }
}
