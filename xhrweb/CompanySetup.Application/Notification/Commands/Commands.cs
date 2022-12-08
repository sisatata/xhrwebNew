using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Notification.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateNotification : IRequest<NotificationCommandVM>
            {
                public Guid? ApplicationId { get; set; }
                public Guid? ApplicantId { get; set; }
                public Guid? ManagerId { get; set; }
                public string NotificationTitle { get; set; }
                public string NotificationDetail { get; set; }
                public Boolean IsViewed { get; set; }
                public DateTime? ViewedTime { get; set; }
                public Int16? NotificationTypeId { get; set; }
                public Guid? NotificationOwnerId { get; set; }
                public Boolean IsPushed { get; set; }
                public DateTime? PushedTime { get; set; }
            }

            public class UpdateNotification : IRequest<NotificationCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? ApplicationId { get; set; }
                public Guid? ApplicantId { get; set; }
                public Guid? ManagerId { get; set; }
                public string NotificationTitle { get; set; }
                public string NotificationDetail { get; set; }
                public Boolean IsViewed { get; set; }
                public DateTime? ViewedTime { get; set; }
                public Int16? NotificationTypeId { get; set; }
                public Guid? NotificationOwnerId { get; set; }
                public Boolean IsPushed { get; set; }
                public DateTime? PushedTime { get; set; }
            }

            public class MarkAsViewedNotification : IRequest<NotificationCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class ProcessHolidayNotificationBulk : IRequest<NotificationCommandVM>
            {
                public Guid? CompanyId { get; set; }
                public DateTime ProcessDate { get; set; }
            }

            public class ProcessOfficeNoticeNotificationBulk : IRequest<NotificationCommandVM>
            {
                public Guid? CompanyId { get; set; }
                public DateTime ProcessDate { get; set; }
            }

            public class MarkAsPushedNotificationBulk : IRequest<NotificationCommandVM>
            {
                public List<Guid> Ids { get; set; }
            }
        }
    }
}
