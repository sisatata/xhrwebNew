using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Notification.Queries.Models
{
   public class NotificationSummary
    {
        public List<NotificationVM> NotificationList { get; set; }
        public short ReadCount { get; set; }
        public short UnReadCount { get; set; }
    }
}
