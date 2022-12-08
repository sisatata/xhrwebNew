using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BonusConfiguration.Queries.Models
{
    public class BonusConfigurationVM
    {
        public Guid? Id { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? ReligionId { get; set; }
        public Int32? RangeFromInMonth { get; set; }
        public Int32? RangeToInMonth { get; set; }
        public int BasicOrGrossId { get; set; }
        public Int32? PercentOfBasicOrGross { get; set; }
        public Int32? FixedAmount { get; set; }
        public Boolean IsPaidFull { get; set; }
        public int PartialOnId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remarks { get; set; }
        public Boolean IsDeleted { get; set; }

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

    }
}
