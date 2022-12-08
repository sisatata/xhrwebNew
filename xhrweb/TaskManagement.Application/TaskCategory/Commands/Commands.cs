using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.TaskCategory.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateTaskCategory : IRequest<TaskCategoryCommandVM>
            {
                public string TaskCategoryName { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateTaskCategory : IRequest<TaskCategoryCommandVM>
            {
                public Guid? Id { get; set; }
                public string TaskCategoryName { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteTaskCategory : IRequest<TaskCategoryCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
