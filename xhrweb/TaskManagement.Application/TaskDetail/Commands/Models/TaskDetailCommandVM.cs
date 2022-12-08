using System;


namespace TaskManagement.Application.TaskDetail.Commands
{
    public class TaskDetailCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
