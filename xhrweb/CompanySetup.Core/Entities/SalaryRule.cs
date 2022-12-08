using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class SalaryRule : BaseEntity<Guid>, IAggregateRoot
    {
        public string RuleName { get; private set; }
        public string Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public SalaryRule(Guid id) : base(id) { }
        private SalaryRule() : base(Guid.NewGuid()) { }

        public static SalaryRule Create(

         string ruleName,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid? companyId

        )

        {
            var oModel = new SalaryRule(Guid.NewGuid());

            oModel.RuleName = ruleName;
            oModel.Description = description;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateSalaryRule
            (

         string ruleName,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid? companyId

        )
        {
            RuleName = ruleName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteSalaryRule()
        {
            IsDeleted = true;
        }


    }
}

