using EmployeeEnrollment.Application.EmployeeExperience .Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeExperience .Queries
{
    public static class Queries
    {
        public class GetEmployeeExperienceList : IRequest< List< EmployeeExperienceVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeExperience : IRequest< EmployeeExperienceVM>
        {
            public Guid Id { get; set; }
        }
    }
}
