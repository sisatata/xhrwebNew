using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class CustomConfiguration : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string Code { get; private set; }
        public string CustomValue { get; private set; }
        public string Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid CompanyId { get; private set; }


        public CustomConfiguration(Guid id) : base(id) { }
        private CustomConfiguration() : base(Guid.NewGuid()) { }

        public static CustomConfiguration Create(

         string code,
         string customValue,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid companyId

        )

        {
            var oModel = new CustomConfiguration(Guid.NewGuid());

            oModel.Code = code;
            oModel.CustomValue = customValue;
            oModel.Description = description;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateCustomConfiguration
            (

         string code,
         string customValue,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid companyId

        )
        {
            Code = code;
            CustomValue = customValue;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteCustomConfiguration()
        {
            IsDeleted = true;
        }


    }
}

