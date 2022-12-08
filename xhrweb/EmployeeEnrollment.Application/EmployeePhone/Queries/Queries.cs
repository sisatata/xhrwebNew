using EmployeeEnrollment.Application.EmployeePhone.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeePhone.Queries
{
    public static class Queries
    {
        public class GetEmployeePhoneList : IRequest<List<EmployeePhoneVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeePhone : IRequest<EmployeePhoneVM>
        {
            public Guid Id { get; set; }
        }
    }
}
