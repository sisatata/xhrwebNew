using EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Queries
{
    public static class Queries
    {
        public class GetPreviousPFGratuityEarnLeaveList : IRequest<List<PreviousPFGratuityEarnLeaveVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetPreviousPFGratuityEarnLeave : IRequest<PreviousPFGratuityEarnLeaveVM>
        {
            public Guid EmployeeId { get; set; }
        }
    }
}
