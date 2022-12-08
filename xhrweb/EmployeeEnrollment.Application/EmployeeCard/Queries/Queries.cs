using EmployeeEnrollment.Application.EmployeeCard.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeCard.Queries
{
    public static class Queries
    {
        public class GetEmployeeCardList : IRequest<List<EmployeeCardVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeCard : IRequest<EmployeeCardVM>
        {
            public Guid Id { get; set; }
        }
    }
}
