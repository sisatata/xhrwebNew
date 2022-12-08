using EmployeeEnrollment.Application.EmployeeImage.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeImage.Queries
{
    public static class Queries
    {
        public class GetEmployeeImageList : IRequest<List<EmployeeImageVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeImage : IRequest<EmployeeImageVM>
        {
            public Guid EmployeeId { get; set; }
        }
    }
}
