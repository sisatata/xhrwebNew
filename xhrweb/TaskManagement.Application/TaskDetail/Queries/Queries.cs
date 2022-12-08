using TaskManagement.Application.TaskDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace TaskManagement.Application.TaskDetail.Queries
{
    public static class Queries
    {
        public class GetTaskDetailList : IRequest<List<TaskDetailVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid? ParentTaskId { get; set; }

            public Guid? UserId { get; set; }
            public Guid? EmployeeId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class GetTaskDetail : IRequest<TaskDetailVM>
        {
            public Guid Id { get; set; }
        }
        public class GetTaskDetailByCompany : IRequest<List<TaskDetailVM>>
        {
            public Guid CompanyId { get; set; }
        }
    }
}
