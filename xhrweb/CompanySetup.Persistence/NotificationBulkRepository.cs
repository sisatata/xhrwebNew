using ASL.Hrms.SharedKernel.Enums;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.NotificationAggregate;
using CompanySetup.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanySetup.Persistence
{
    public class NotificationBulkRepository : INotificationBulkRepository
    {
        public NotificationBulkRepository(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public CompanyContext _companyContext { get; }

        public async Task InsertUpdate(NotificationAggregate notificationAggregate)
        {
            foreach (var item in notificationAggregate.NotificationList)
            {
                if (item.State == TrackingState.Added)
                {
                    _companyContext.Entry(item).State = EntityState.Added;
                }
                if (item.State == TrackingState.Modified)
                {
                    _companyContext.Entry(item).State = EntityState.Modified;
                }
                if (item.State == TrackingState.Deleted)
                {
                    _companyContext.Entry(item).State = EntityState.Deleted;
                }
            }

            await _companyContext.SaveChangesAsync();
        }

        public async Task InsertUpdate(List<Notification> notifications)
        {
            foreach (var item in notifications)
            {
                if (item.State == TrackingState.Added)
                {
                    _companyContext.Entry(item).State = EntityState.Added;
                }
                if (item.State == TrackingState.Modified)
                {
                    _companyContext.Entry(item).State = EntityState.Modified;
                }
                if (item.State == TrackingState.Deleted)
                {
                    _companyContext.Entry(item).State = EntityState.Deleted;
                }
            }

            await _companyContext.SaveChangesAsync();
        }
    }
}
