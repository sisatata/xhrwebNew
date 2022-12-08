using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Notification.Queries.Models
{
    public class NotificationVM
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
        public string NotificationOwnerAccessToken { get;  set; }
    }
}
