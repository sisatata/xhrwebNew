using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class BenefitDeductionConfig : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {

        public string BenefitDeductionCode { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public string BasicOrGross { get; private set; }
        public string CalculationType { get; private set; }
        public decimal? PercentOfBasicOrGross { get; private set; }
        public decimal? FixedAmount { get; private set; }
        public Int32? IntervalId { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsCalculateSalary { get; private set; }
        public Boolean IsActive { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public BenefitDeductionConfig(Guid id) : base(id) { }
        private BenefitDeductionConfig() : base(Guid.NewGuid()) { }

        public static BenefitDeductionConfig Create(


         string benefitDeductionCode,
         string name,
         string description,
         string type,
         string basicOrGross,
         string calculationType,
         decimal? percentOfBasicOrGross,
         decimal? fixedAmount,
         Int32? intervalId,
         Guid? companyId,
         Boolean isCalculateSalary,
         Boolean isActive,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted

        )

        {
            var oModel = new BenefitDeductionConfig(Guid.NewGuid());


            oModel.BenefitDeductionCode = benefitDeductionCode;
            oModel.Name = name;
            oModel.Description = description;
            oModel.Type = type;
            oModel.BasicOrGross = basicOrGross;
            oModel.CalculationType = calculationType;
            oModel.PercentOfBasicOrGross = percentOfBasicOrGross;
            oModel.FixedAmount = fixedAmount;
            oModel.IntervalId = intervalId;
            oModel.CompanyId = companyId;
            oModel.IsCalculateSalary = isCalculateSalary;
            oModel.IsActive = isActive;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdateBenefitDeductionConfig
            (


         string benefitDeductionCode,
         string name,
         string description,
         string type,
         string basicOrGross,
         string calculationType,
         decimal? percentOfBasicOrGross,
         decimal? fixedAmount,
         Int32? intervalId,
         Guid? companyId,
         Boolean isCalculateSalary,
         Boolean isActive,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted

        )
        {

            BenefitDeductionCode = benefitDeductionCode;
            Name = name;
            Description = description;
            Type = type;
            BasicOrGross = basicOrGross;
            CalculationType = calculationType;
            PercentOfBasicOrGross = percentOfBasicOrGross;
            FixedAmount = fixedAmount;
            IntervalId = intervalId;
            CompanyId = companyId;
            IsCalculateSalary = isCalculateSalary;
            IsActive = isActive;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteBenefitDeductionConfig()
        {
            IsDeleted = true;
        }


    }
}

