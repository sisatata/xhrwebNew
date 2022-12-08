using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Events
{
    public class HolidayPushNotificationProcessEvent : ICommand
    {
        public Guid? CompanyId { get; set; }
        public DateTime HolidayDate { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
