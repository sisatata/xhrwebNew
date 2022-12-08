using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.NotificationAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanySetup.Core.Interfaces
{
   public interface INotificationBulkRepository
    {
        Task InsertUpdate(NotificationAggregate notificationAggregate);
        Task InsertUpdate(List<Notification> notifications);
    }
}
