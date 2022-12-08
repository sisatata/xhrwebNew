using System;


namespace TaskManagement.Application.TaskCategory.Commands
{
    public class TaskCategoryCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
