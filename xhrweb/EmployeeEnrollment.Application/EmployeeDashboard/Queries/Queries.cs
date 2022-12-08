using EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries
{
    public static class Queries
    {
        public class GetEmployeeDashboard : IRequest<EmployeeDashboardVM>
        {
            public Guid Id { get; set; }
        }

        public class GetEmployeeProfileById : IRequest<EmployeeProfileVM>
        {
            public Guid EmployeeId { get; set; }
        }
    }
}
