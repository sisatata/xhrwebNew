using System;


namespace TaskManagement.Application.TaskDetailLog.Commands
{
    public class TaskDetailLogCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
