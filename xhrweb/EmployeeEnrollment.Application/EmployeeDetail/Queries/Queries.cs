using EmployeeEnrollment.Application.EmployeeDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeDetail.Queries
{
    public static class Queries
    {
        public class GetEmployeeDetailList : IRequest<List<EmployeeDetailVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeDetailByEmployee : IRequest<EmployeeDetailVM>
        {
            public Guid EmployeeId { get; set; }
        }
    }
}
