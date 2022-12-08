using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models
{
    public class NotificationSummaryVM
    {
        public List<NotificationVM> NotificationList { get; set; }
        public short ReadCount { get; set; }
        public short UnReadCount { get; set; }
    }
}
