using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.TaskDetailLog.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateTaskDetailLog : IRequest<TaskDetailLogCommandVM>
            {
                public Guid? TaskDetailId { get; set; }
                public string UpdateInfo { get; set; }
                public DateTime? DateUpdated { get; set; }
                public Guid? EmployeeId { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public DateTime? SpendTime { get; set; }
            }

            public class UpdateTaskDetailLog : IRequest<TaskDetailLogCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? TaskDetailId { get; set; }
                public string UpdateInfo { get; set; }
                public DateTime? DateUpdated { get; set; }
                public Guid? EmployeeId { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public DateTime? SpendTime { get; set; }
            }

            public class MarkAsDeleteTaskDetailLog : IRequest<TaskDetailLogCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
