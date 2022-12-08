using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.TaskDetail.Queries.Models;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models
{
    public class EmployeeDashboardVM
    {
        public Guid EmployeeId { get; set; }
        public DailyAttendanceSummaryVM EmployeeAttendance { get; set; }
        public List<EmployeeLeaveVM> EmployeeLeaves { get; set; }
        public List<TaskDetailVM> Tasks { get; set; }
        public List<BenefitBillClaimSummaryVM> BenefitBillClaimSummaries { get; set; }
        public NotificationSummaryVM NotificationSummary { get; set; }

        public List<OfficeNoticeVM> OfficeNotices { get; set; }
    }
}
