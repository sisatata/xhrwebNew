using EmployeeEnrollment.Application.EmployeeRawDataHist.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeRawDataHist.Queries
{
    public static class Queries
    {
        public class GetEmployeeRawDataHistList : IRequest<List<EmployeeRawDataHistVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeRawDataHist : IRequest<EmployeeRawDataHistVM>
        {
            public Guid Id { get; set; }
        }
    }
}
