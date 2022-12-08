using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class ActivityLog : BaseEntity<Guid>, IAggregateRoot
    {
        //public Guid? Id { get; private set; }
        public Guid? ActivityLogTypeId { get;  set; }
        public Guid? UserId { get;  set; }
        public Guid? CompanyId { get;  set; }
        public string Comment { get;  set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public Guid? EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity name
        /// </summary>
        public string EntityName { get; set; }
        public ActivityLog(Guid id) : base(id) { }
        private ActivityLog() : base(Guid.NewGuid()) { }

        public static ActivityLog Create(

         Guid? companyId,
         Guid? activityLogTypeId,
         Guid? userId,
         string comment,
         string ipAddress,
            Guid? entityId
        )

        {
            var oModel = new ActivityLog(Guid.NewGuid());

            oModel.ActivityLogTypeId = activityLogTypeId;
            oModel.UserId = userId;
            oModel.Comment = comment;
            oModel.IpAddress = ipAddress;
            oModel.CreatedDate = DateTime.Now;
            oModel.CompanyId = companyId;
            oModel.EntityId = entityId;
            return oModel;

        }


        public void UpdateActivityLog
            (
         Guid? companyId,
         Guid? activityLogTypeId,
         Guid? userId,
         string comment,
         string ipAddress,
         Guid? entityId

        )
        {
            ActivityLogTypeId = activityLogTypeId;
            UserId = userId;
            Comment = comment;
            IpAddress = ipAddress;
            CompanyId = companyId;
            EntityId = entityId;
        }


        public void MarkAsDeleteActivityLog()
        {
            //IsDeleted = true;
        }


    }
}

