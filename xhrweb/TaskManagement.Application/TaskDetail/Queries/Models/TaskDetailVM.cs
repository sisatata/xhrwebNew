using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.TaskDetail.Queries.Models
{
    public class TaskDetailVM
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
        public Guid? TaskCreator { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedBy { get; set; }
        public Guid? AssignedById { get; set; }
        public string TaskCategoryName { get; set; }
        public string FullName { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int? Progress { get; set; }
    }
}
