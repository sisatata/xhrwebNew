using LeaveManagement.Application.LeaveTypeGroup.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace LeaveManagement.Application.LeaveTypeGroup.Queries
{
    public static class Queries
    {
        public class GetLeaveTypeGroupList : IRequest<List<LeaveTypeGroupVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetLeaveTypeGroup : IRequest<LeaveTypeGroupVM>
        {
            public int Id { get; set; }
        }
    }
}
