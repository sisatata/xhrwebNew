using TaskManagement.Application.TaskCategory.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace TaskManagement.Application.TaskCategory.Queries
{
    public static class Queries
    {
        public class GetTaskCategoryList : IRequest<List<TaskCategoryVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetTaskCategory : IRequest<TaskCategoryVM>
        {
            public Guid Id { get; set; }
        }
    }
}
