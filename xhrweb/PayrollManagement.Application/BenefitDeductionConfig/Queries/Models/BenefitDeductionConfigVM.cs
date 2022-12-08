using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitDeductionConfig.Queries.Models
{
    public class BenefitDeductionConfigVM
    {
        public Guid? Id { get; set; }
        public string BenefitDeductionCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string BasicOrGross { get; set; }
        public string CalculationType { get; set; }
        public decimal? PercentOfBasicOrGross { get; set; }
        public decimal? FixedAmount { get; set; }
        public Int32? IntervalId { get; set; }
        public Guid? CompanyId { get; set; }
        public Boolean IsCalculateSalary { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
