using CompanySetup.Application.ActivityLogType.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.ActivityLogType.Queries
{
    public static class Queries
    {
        public class GetActivityLogTypeList : IRequest<List<ActivityLogTypeVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetActivityLogType : IRequest<ActivityLogTypeVM>
        {
            public Guid Id { get; set; }
        }
    }
}
