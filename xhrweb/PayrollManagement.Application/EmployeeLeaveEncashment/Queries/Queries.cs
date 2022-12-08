using PayrollManagement.Application.EmployeeLeaveEncashment.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeeLeaveEncashment.Queries
{
    public static class Queries
    {
        public class GetEmployeeLeaveEncashmentList : IRequest<List<EmployeeLeaveEncashmentVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeLeaveEncashment : IRequest<EmployeeLeaveEncashmentVM>
        {
            public Guid Id { get; set; }
        }
    }
}
