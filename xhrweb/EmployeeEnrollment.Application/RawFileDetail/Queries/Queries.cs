using EmployeeEnrollment.Application.RawFileDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.RawFileDetail.Queries
{
    public static class Queries
    {
        public class GetRawFileDetailList : IRequest<List<RawFileDetailVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetRawFileDetail : IRequest<RawFileDetailVM>
        {
            public Guid Id { get; set; }
        }
    }
}
