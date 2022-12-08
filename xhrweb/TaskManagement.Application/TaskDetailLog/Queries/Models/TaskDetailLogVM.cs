using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.TaskDetailLog.Queries.Models
{
    public class TaskDetailLogVM
    {
        public Guid? Id { get; set; }
        public Guid? TaskDetailId { get; set; }
        public string UpdateInfo { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? SpendTime { get; set; }
        public string EmployeeName { get; set; }
    }
}
