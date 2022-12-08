using EmployeeEnrollment.Application.EmployeeAddress.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeAddress.Queries
{
    public static class Queries
    {
        public class GetEmployeeAddressList : IRequest<List<EmployeeAddressVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeAddress : IRequest<EmployeeAddressVM>
        {
            public Guid Id { get; set; }
        }
    }
}
