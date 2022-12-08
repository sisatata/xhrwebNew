using CompanySetup.Application.Notification.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.Notification.Queries
{
    public static class Queries
    {
        public class GetNotificationListByOwner : IRequest<List<NotificationVM>>
        {
            public Guid NotificationOwnerId { get; set; }
        }

        public class GetNotificationSummaryByOwner : IRequest<NotificationSummary>
        {
            public Guid NotificationOwnerId { get; set; }
        }
        public class GetNotification : IRequest<NotificationVM>
        {
            public Guid Id { get; set; }
        }

        public class GetPendingNotificationListToPush : IRequest<List<NotificationVM>>
        {
            
        }
    }
}
