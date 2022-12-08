using ASL.Utility.FileManager.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TaskManagement.Application.TaskDetail.Queries.Models;

namespace TaskManagement.Application.TaskDetail.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateTaskDetail : IRequest<TaskDetailCommandVM>
            {
                public Guid? TaskTypeId { get; set; }
                public string TaskName { get; set; }
                public string TaskDescription { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Guid? AssigneeId { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? ParentTaskId { get; set; }
                public Guid? TaskCreator { get; set; }
                public int? Progress { get; set; }
            }

            public class UpdateTaskDetail : IRequest<TaskDetailCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? TaskTypeId { get; set; }
                public string TaskName { get; set; }
                public string TaskDescription { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Guid? AssigneeId { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? ParentTaskId { get; set; }
                public int StatusId { get; set; }
                public int? Progress { get; set; }
            }

            public class MarkAsDeleteTaskDetail : IRequest<TaskDetailCommandVM>
            {
                public Guid Id { get; set; }
            }
            public class TaskDetailListExcel : IRequest<List<TaskDetailListExportVM>>
            {
                public Guid CompanyId { get; set; }
                public Guid? ParentTaskId { get; set; }
                public Guid? UserId { get; set; }
                public Guid? EmployeeId { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }

            }
            public class TaskDetailListExportVM : IExcelDataDynamicType
            {

                [Description("Task Name")]
                public string TaskName { get; set; }
                [Description("Description")]
                public string TaskDescription { get; set; }
                [Description("Start Date")]
                public DateTime? StartDate { get; set; }
                [Description("End Date")]
                public DateTime? EndDate { get; set; }

                [Description("Assigned By")]
                public string AssignedBy { get; set; }
                [Description("Assignee")]
                public string FullName { get; set; }
                [Description("Task Category")]
                public string TaskCategoryName { get; set; }
               
                public string Status { get; set; }
                [Description("Progress (%)")]
                public int? Progress { get; set; }
                public bool IsBoldRow { get; set; }
                public bool WillRemoveColumn { get; set; }

            }
        }
    }
}
