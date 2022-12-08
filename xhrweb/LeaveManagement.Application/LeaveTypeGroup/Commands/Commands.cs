using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveTypeGroup.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateLeaveTypeGroup : IRequest<LeaveTypeGroupCommandVM>
            {
                public string LeaveTypeGroupName { get; set; }
                public string LeaveTypeGroupNameLC { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class UpdateLeaveTypeGroup : IRequest<LeaveTypeGroupCommandVM>
            {
                public int Id { get; set; }
                public string LeaveTypeGroupName { get; set; }
                public string LeaveTypeGroupNameLC { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class MarkAsDeleteLeaveTypeGroup : IRequest<LeaveTypeGroupCommandVM>
            {
                public int Id { get; set; }
            }
        }
    }
}
