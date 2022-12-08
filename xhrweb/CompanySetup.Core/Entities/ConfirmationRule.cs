using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class ConfirmationRule : BaseEntity<Guid>, IAggregateRoot
    {
        public string RuleName { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Int16? ConfirmationType { get; private set; }
        public Int16? ConfirmationMonths { get; private set; }
        public Int16? SortOrder { get; private set; }
        public Boolean IsActive { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public ConfirmationRule(Guid id) : base(id) { }
        private ConfirmationRule() : base(Guid.NewGuid()) { }

        public static ConfirmationRule Create(

         string ruleName,
         string description,
         DateTime startDate,
         DateTime endDate,
         Int16? confirmationType,
         Int16? confirmationMonths,
         Int16? sortOrder,
         Boolean isActive,
         Boolean isDeleted,
         Guid? companyId

        )

        {
            var oModel = new ConfirmationRule(Guid.NewGuid());

            oModel.RuleName = ruleName;
            oModel.Description = description;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.ConfirmationType = confirmationType;
            oModel.ConfirmationMonths = confirmationMonths;
            oModel.SortOrder = sortOrder;
            oModel.IsActive = isActive;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateConfirmationRule
            (

         string ruleName,
         string description,
         DateTime startDate,
         DateTime endDate,
         Int16? confirmationType,
         Int16? confirmationMonths,
         Int16? sortOrder,
         Boolean isActive,
         Boolean isDeleted,
         Guid? companyId

        )
        {
            RuleName = ruleName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            ConfirmationType = confirmationType;
            ConfirmationMonths = confirmationMonths;
            SortOrder = sortOrder;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteConfirmationRule()
        {
            IsDeleted = true;
        }


    }
}

