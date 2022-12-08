using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.IncomeTaxInvestment.Queries.Models
{
    public class IncomeTaxInvestmentVM
    {
        public Guid? Id { get; set; }
        public decimal? InvestmentPercentage { get; set; }
        public decimal? WaiverPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remarks { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
