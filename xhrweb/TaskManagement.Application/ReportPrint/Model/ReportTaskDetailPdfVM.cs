using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.ReportPrint.Model
{
   public class ReportTaskDetailPdfVM
    {
        public Guid? Id { get; set; }
        public Guid? TaskTypeId { get; set; }
        public int? SortOrder { get; set; }
        public string EmployeeId { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
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
