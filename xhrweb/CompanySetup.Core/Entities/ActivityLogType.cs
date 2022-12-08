using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class ActivityLogType : BaseEntity<Guid>, IAggregateRoot
    {
        public string SystemKeyword { get; private set; }
        public string Name { get; private set; }
        public Boolean Enabled { get; private set; }


        public ActivityLogType(Guid id) : base(id) { }
        private ActivityLogType() : base(Guid.NewGuid()) { }

        public static ActivityLogType Create(

         string systemKeyword,
         string name,
         Boolean enabled

        )

        {
            var oModel = new ActivityLogType(Guid.NewGuid());
            oModel.SystemKeyword = systemKeyword;
            oModel.Name = name;
            oModel.Enabled = enabled;
            return oModel;
        }


        public void UpdateActivityLogType
            (

         string systemKeyword,
         string name,
         Boolean enabled


        )
        {
            SystemKeyword = systemKeyword;
            Name = name;
            Enabled = enabled;
        }


        public void MarkAsDeleteActivityLogType()
        {
            // IsDeleted = true;
        }


    }
}

