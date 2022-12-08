using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.ActivityLogType.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateActivityLogType : IRequest<ActivityLogTypeCommandVM>
            {
                public string SystemKeyword { get; set; }
                public string Name { get; set; }
                public Boolean Enabled { get; set; }
            }

            public class UpdateActivityLogType : IRequest<ActivityLogTypeCommandVM>
            {
                public Guid? Id { get; set; }
                public string SystemKeyword { get; set; }
                public string Name { get; set; }
                public Boolean Enabled { get; set; }
            }

            public class MarkAsDeleteActivityLogType : IRequest<ActivityLogTypeCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
