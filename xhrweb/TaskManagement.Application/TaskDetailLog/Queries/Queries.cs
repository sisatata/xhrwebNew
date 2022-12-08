using TaskManagement.Application.TaskDetailLog.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace TaskManagement.Application.TaskDetailLog.Queries
{
    public static class Queries
    {
        public class GetTaskDetailLogList : IRequest<List<TaskDetailLogVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetTaskDetailLog : IRequest<TaskDetailLogVM>
        {
            public Guid Id { get; set; }
        }
        public class GetTaskDetailLogListByTaskDetail : IRequest<List<TaskDetailLogVM>>
        {
          
            public Guid TaskDetailId { get; set; }
        }
    }
}
