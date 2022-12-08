using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using PayrollManagement.Core.ExtensionMethods;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollManagement.Core.Entities.BonusConfigurationAggregate
{
    public class BonusConfigurationAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        public BonusConfiguration BonusConfiguration { get; set; }
        private IReadOnlyList<BonusConfiguration> _bonusConfigurationDB { get; set; }
        public BonusConfigurationAggregate(IReadOnlyList<BonusConfiguration> bonusConfigurationDB)
        {
            BonusConfiguration = new BonusConfiguration(Guid.NewGuid());
            _bonusConfigurationDB = bonusConfigurationDB;
        }

        public BonusConfigurationAggregate(BonusConfiguration bonusConfiguration)
        {
            BonusConfiguration = bonusConfiguration;
        }

        public void NewBonusConfiguration(Guid? companyId,
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
         string remarks)
        {
            Guard.Against.NullOrEmptyGuid(companyId.Value, nameof(companyId));
            Guard.Against.Null(startDate, nameof(startDate));
            Guard.Against.Null(endDate, nameof(endDate));
            Guard.Against.Null(rangeFromInMonth, nameof(rangeFromInMonth));
            Guard.Against.Null(rangeToInMonth, nameof(rangeToInMonth));
            Guard.Against.InvalidStartDateEndDate(startDate.Value, endDate.Value, nameof(endDate), "End date must be greater or equal than start date.");


            Guard.Against.DuplicateRecords(_bonusConfigurationDB.ToList(), startDate.Value, endDate.Value,rangeFromInMonth.Value,rangeToInMonth.Value, "Duplicate start/end date");

            BonusConfiguration.Create(companyId,
                                    religionId,
                                    rangeFromInMonth,
                                    rangeToInMonth,
                                    basicOrGrossId,
                                    percentOfBasicOrGross,
                                    fixedAmount,
                                    isPaidFull,
                                    partialOnId,
                                    startDate,
                                    endDate,
                                    remarks);
        }

        public void UpdateBonusConfiguration(BonusConfiguration bonusConfiguration, Guid? companyId,
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
         string remarks)
        {
            Guard.Against.NullOrEmptyGuid(companyId.Value, nameof(companyId));
            Guard.Against.Null(startDate, nameof(startDate));
            Guard.Against.Null(endDate, nameof(endDate));
            Guard.Against.InvalidStartDateEndDate(startDate.Value, endDate.Value, nameof(endDate), "End date must be greater or equal than start date.");
            Guard.Against.DuplicateRecordInUpdate(_bonusConfigurationDB.ToList(), bonusConfiguration.Id, startDate.Value, endDate.Value, rangeFromInMonth.Value, rangeToInMonth.Value, "Duplicate start/end date");

            bonusConfiguration.UpdateBonusConfiguration(religionId,
                                    rangeFromInMonth,
                                    rangeToInMonth,
                                    basicOrGrossId,
                                    percentOfBasicOrGross,
                                    fixedAmount,
                                    isPaidFull,
                                    partialOnId,
                                    startDate,
                                    endDate,
                                    remarks);
            BonusConfiguration = bonusConfiguration;
        }
    }
}
