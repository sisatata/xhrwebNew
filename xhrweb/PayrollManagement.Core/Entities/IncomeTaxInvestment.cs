using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class IncomeTaxInvestment : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public decimal? InvestmentPercentage { get; private set; }
        public decimal? WaiverPercentage { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Remarks { get; private set; }
        public Guid? CompanyId { get; private set; }

        public Boolean IsDeleted { get; set; }
        public IncomeTaxInvestment(Guid id) : base(id) { }
        private IncomeTaxInvestment() : base(Guid.NewGuid()) { }

        public static IncomeTaxInvestment Create(

         decimal? investmentPercentage,
         decimal? waiverPercentage,
         DateTime? startDate,
         DateTime? endDate,
         string remarks,
         Guid? companyId

        )

        {
            var oModel = new IncomeTaxInvestment(Guid.NewGuid());

            oModel.InvestmentPercentage = investmentPercentage;
            oModel.WaiverPercentage = waiverPercentage;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.Remarks = remarks;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateIncomeTaxInvestment
            (

         decimal? investmentPercentage,
         decimal? waiverPercentage,
         DateTime? startDate,
         DateTime? endDate,
         string remarks,
         Guid? companyId
    
        )
        {
            
            InvestmentPercentage = investmentPercentage;
            WaiverPercentage = waiverPercentage;
            StartDate = startDate;
            EndDate = endDate;
            Remarks = remarks;
            CompanyId = companyId;
        }


        public void MarkAsDeleteIncomeTaxInvestment()
        {
            IsDeleted = true;
        }


    }
}

