using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class DefaultConfiguration : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string Code { get; private set; }
        public string DefaultValue { get; private set; }
        public string Description { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public DefaultConfiguration(Guid id) : base(id) { }
        private DefaultConfiguration() : base(Guid.NewGuid()) { }

        public static DefaultConfiguration Create(

         string code,
         string defaultValue,
         string description,
         Boolean isDeleted

        )

        {
            var oModel = new DefaultConfiguration(Guid.NewGuid());

            oModel.Code = code;
            oModel.DefaultValue = defaultValue;
            oModel.Description = description;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdateDefaultConfiguration
            (

         string code,
         string defaultValue,
         string description,
         Boolean isDeleted

        )
        {
            Code = code;
            DefaultValue = defaultValue;
            Description = description;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteDefaultConfiguration()
        {
            IsDeleted = true;
        }


    }
}

