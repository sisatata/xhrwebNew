using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.ActivityLog.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateActivityLog : IRequest<ActivityLogCommandVM>
            {
                public string SystemKeyword { get; set; }
                public Guid? UserId { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? EntityId { get; set; }
                public string Comment { get; set; }
                public string IpAddress { get; set; }
            }

            public class UpdateActivityLog : IRequest<ActivityLogCommandVM>
            {
                public Guid? Id { get; set; }
                public string SystemKeyword { get; set; }
                public Guid? UserId { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? EntityId { get; set; }
                public string Comment { get; set; }
                public string IpAddress { get; set; }
            }

            public class MarkAsDeleteActivityLog : IRequest<ActivityLogCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
