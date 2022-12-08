using EmployeeEnrollment.Application.EmployeeStatusHistory.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeStatusHistory.Queries
{
    public static class Queries
    {
        public class GetEmployeeStatusHistoryList : IRequest<List<EmployeeStatusHistoryVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeStatusHistory : IRequest<EmployeeStatusHistoryVM>
        {
            public Guid Id { get; set; }
        }
    }
}
