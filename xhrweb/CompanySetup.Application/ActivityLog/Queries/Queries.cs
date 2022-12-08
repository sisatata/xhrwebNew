using CompanySetup.Application.ActivityLog.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.ActivityLog.Queries
{
    public static class Queries
    {
        public class GetActivityLogList : IRequest<List<ActivityLogVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetActivityLog : IRequest<ActivityLogVM>
        {
            public Guid Id { get; set; }
        }
    }
}
