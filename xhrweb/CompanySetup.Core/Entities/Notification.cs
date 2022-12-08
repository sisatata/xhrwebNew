using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class Notification : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        //public Guid? Id { get; private set; }
        public Guid? ApplicationId { get; private set; }
        public Guid? ApplicantId { get; private set; }
        public Guid? ManagerId { get; private set; }
        public string NotificationTitle { get; private set; }
        public string NotificationDetail { get; private set; }
        public Boolean IsViewed { get; private set; }
        public DateTime? ViewedTime { get; private set; }
        public Int16? NotificationTypeId { get; private set; }

        public Guid? NotificationOwnerId { get; private set; }
        public string NotificationOwnerAccessToken { get; private set; }

        public Boolean IsPushed { get; private set; }
        public DateTime? PushedTime { get; private set; }

        // not persisted
        public TrackingState State { get; set; }
        public Notification(Guid id) : base(id) { }
        private Notification() : base(Guid.NewGuid()) { }

        public static Notification Create(

         Guid? applicationId,
         Guid? applicantId,
         Guid? managerId,
         string notificationTitle,
         string notificationDetail,
         Boolean isViewed,
         DateTime? viewedTime,
         Int16? notificationTypeId,
         Guid? notificationOwnerId,
         Boolean isPushed,
         DateTime? pushedTime
        )

        {
            var oModel = new Notification(Guid.NewGuid());
            oModel.ApplicationId = applicationId;
            oModel.ApplicantId = applicantId;
            oModel.ManagerId = managerId;
            oModel.NotificationTitle = notificationTitle;
            oModel.NotificationDetail = notificationDetail;
            oModel.IsViewed = isViewed;
            oModel.ViewedTime = viewedTime;
            oModel.NotificationTypeId = notificationTypeId;
            oModel.NotificationOwnerId = notificationOwnerId;
            oModel.IsPushed = isPushed;
            oModel.PushedTime = pushedTime;
            return oModel;

        }


        public void UpdateNotification
            (

         Guid? applicationId,
         Guid? applicantId,
         Guid? managerId,
         string notificationTitle,
         string notificationDetail,
         Boolean isViewed,
         DateTime? viewedTime,
         Int16? notificationTypeId,
         Guid? notificationOwnerId,
         Boolean isPushed,
         DateTime? pushedTime
        )
        {
            ApplicationId = applicationId;
            ApplicantId = applicantId;
            ManagerId = managerId;
            NotificationTitle = notificationTitle;
            NotificationDetail = notificationDetail;
            IsViewed = isViewed;
            ViewedTime = viewedTime;
            NotificationTypeId = notificationTypeId;
            NotificationOwnerId = notificationOwnerId;
            IsPushed = isPushed;
            PushedTime = pushedTime;
        }


        public void MarkAsViewedNotification()
        {
            IsViewed = true;
            ViewedTime = DateTime.Now;
            State = TrackingState.Modified;
        }

        public void MarkAsPushedNotification()
        {
            IsPushed = true;
            PushedTime = DateTime.Now;
            State = TrackingState.Modified;
        }

        public void StartBulk(
         string notificationTitle,
         string notificationDetail,
         Int16? notificationTypeId,
         Guid? notificationOwnerId,
         string notificationOwnerAccessToken,
         TrackingState state
        )

        {
            NotificationTitle = notificationTitle;
            NotificationDetail = notificationDetail;
            IsViewed = false;
            NotificationTypeId = notificationTypeId;
            NotificationOwnerId = notificationOwnerId;
            NotificationOwnerAccessToken = notificationOwnerAccessToken;
            IsPushed = false;
            State = state;
        }
    }
}

