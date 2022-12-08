using EmployeeEnrollment.Application.EmployeeRawDataPrep .Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeRawDataPrep .Queries
{
    public static class Queries
    {
        public class GetEmployeeRawDataPrepList : IRequest< List< EmployeeRawDataPrepVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeRawDataPrep : IRequest< EmployeeRawDataPrepVM>
        {
            public Guid Id { get; set; }
        }
    }
}
