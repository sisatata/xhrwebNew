using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitDeductionInterval.Queries.Models
{
    public class BenefitDeductionIntervalVM
    {
        public Guid? Id { get; set; }
        public Int32? IntervalId { get; set; }
        public string IntervalName { get; set; }
    }
}
