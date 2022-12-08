using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Entities.BonusConfigurationAggregate
{
    public class BonusConfiguration : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? CompanyId { get; private set; }
        public Guid? ReligionId { get; private set; }
        public Int32? RangeFromInMonth { get; private set; }
        public Int32? RangeToInMonth { get; private set; }
        public int BasicOrGrossId { get; private set; }
        public Int32? PercentOfBasicOrGross { get; private set; }
        public Int32? FixedAmount { get; private set; }
        public Boolean IsPaidFull { get; private set; }
        public int PartialOnId { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsDeleted { get; private set; }

        // not persisted
        public PartialOn PartialOn
        {
            get => (PartialOn)PartialOnId;
            set => PartialOnId = (int)value;
        }

        // not persisted
        public BasicOrGross BasicOrGross
        {
            get => (BasicOrGross)BasicOrGrossId;
            set => BasicOrGrossId = (int)value;
        }

        public BonusConfiguration(Guid id) : base(id) { }
        private BonusConfiguration() : base(Guid.NewGuid()) { }

        public void Create(
         Guid? companyId,
         Guid? religionId,
         Int32? rangeFromInMonth,
         Int32? rangeToInMonth,
         int basicOrGrossId,
         Int32? percentOfBasicOrGross,
         Int32? fixedAmount,
         Boolean isPaidFull,
         int partialOnId,
         DateTime? startDate,
         DateTime? endDate,
         string remarks

        )

        {
            CompanyId = companyId;
            ReligionId = religionId;
            RangeFromInMonth = rangeFromInMonth;
            RangeToInMonth = rangeToInMonth;
            BasicOrGrossId = basicOrGrossId;
            PercentOfBasicOrGross = percentOfBasicOrGross;
            FixedAmount = fixedAmount;
            IsPaidFull = isPaidFull;
            PartialOnId = partialOnId;
            StartDate = startDate;
            EndDate = endDate;
            Remarks = remarks;
            IsDeleted = false;
        }


        public void UpdateBonusConfiguration
            (
         Guid? religionId,
         Int32? rangeFromInMonth,
         Int32? rangeToInMonth,
         int basicOrGrossId,
         Int32? percentOfBasicOrGross,
         Int32? fixedAmount,
         Boolean isPaidFull,
         int partialOnId,
         DateTime? startDate,
         DateTime? endDate,
         string remarks

        )
          {
            ReligionId = religionId;
            RangeFromInMonth = rangeFromInMonth;
            RangeToInMonth = rangeToInMonth;
            BasicOrGrossId = basicOrGrossId;
            PercentOfBasicOrGross = percentOfBasicOrGross;
            FixedAmount = fixedAmount;
            IsPaidFull = isPaidFull;
            PartialOnId = partialOnId;
            StartDate = startDate;
            EndDate = endDate;
            Remarks = remarks;
        }


        public void MarkAsDeleteBonusConfiguration()
        {
            IsDeleted = true;
        }


    }
}

