using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.IncomeTaxInvestment.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateIncomeTaxInvestment : IRequest<IncomeTaxInvestmentCommandVM>
            {
                public decimal? InvestmentPercentage { get; set; }
                public decimal? WaiverPercentage { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateIncomeTaxInvestment : IRequest<IncomeTaxInvestmentCommandVM>
            {
                public Guid? Id { get; set; }
                public decimal? InvestmentPercentage { get; set; }
                public decimal? WaiverPercentage { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteIncomeTaxInvestment : IRequest<IncomeTaxInvestmentCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
